using CsvHelper;
using GlideLog.Data;
using System.Globalization;

namespace GlideLog.Models
{
	public class FlightListModel
	{
		private FlightDatabase _database;
		private CancellationTokenSource cts = new CancellationTokenSource();

        public FlightListModel(FlightDatabase database)
        {
			_database = database;
		}

		public async Task<List<FlightEntryModel>> GetFlightsFromDataBase()
		{
			List<FlightEntryModel> dbFlights = new();
			return await _database.GetFlightsAsync();
		}

		public async Task<bool> DeleteFlightFromDatabaseAsync(FlightEntryModel flightEntryModel)
		{
			if(await _database.DeleteFlightAsync(flightEntryModel) == 1)
			{
				return true;
			}
			return false;
		}


        public async Task<bool> ExportFromDatabaseAsync(string path)
		{

			// Get the flights from the database
			List<FlightEntryModel> dbFlights = await _database.GetFlightsAsync();
			// Strip the ID
			List<CsvFlightEntry> strippedID = new List<CsvFlightEntry>();
			foreach(FlightEntryModel flight in dbFlights)
			{
				strippedID.Add(new CsvFlightEntry()
				{
					DateTime = flight.DateTime,
					Site = flight.Site,
					Glider = flight.Glider,
					FlightCount = flight.FlightCount,
					Hours = flight.Hours,
					Minutes = flight.Minutes,
					OmitFromTotals = flight.OmitFromTotals,
					Notes = flight.Notes
				});
			}

			// Write to csv file
			if(dbFlights.Count > 0)
			{
				try
				{
                    var statusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                    var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

					//string androidExternalStorageDirectory = "/storage/emulated/0/";
					//string androidDocumentsDirectory = androidExternalStorageDirectory + "Documents/";
					//Directory.CreateDirectory(androidGlideLogDirectory);
					//string glideLogFile = Path.Combine(androidGlideLogDirectory, "GlideLog.csv");
                    // Android.OS.Environment.ExternalStorageDirectory
                    //string targetFile = System.IO.Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "GlideLog.csv"); //FileSystem.Current.AppDataDirectory

                    string targetFile = System.IO.Path.Combine(path, "GlideLog.csv");
					
                    if (statusRead == PermissionStatus.Granted && statusWrite == PermissionStatus.Granted)
                    {
                        using (TextWriter writer = new StreamWriter(targetFile))
                        {
                            var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                            csv.WriteHeader<CsvFlightEntry>();
                            csv.NextRecord();
                            csv.WriteRecords(strippedID);
                        }
						return true;
                    }
                }
                catch (Exception ex)
				{
					// TODO: handle exception properly
					string message = ex.Message;	
				}
			}
			return false;
		}

		public async Task<List<FlightEntryModel>> ImportToDatabaseAsync(string path)
		{
			List<CsvFlightEntry> flights = new List<CsvFlightEntry>();
			List<FlightEntryModel> flightsToReturn = new List<FlightEntryModel>();

			using (StreamReader reader = new StreamReader(path))
			{
				using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					// Get the flights from the csv file
					flights = csv.GetRecords<CsvFlightEntry>().ToList();

					// Get the flights from the database
					List<FlightEntryModel> dbFlights = await _database.GetFlightsAsync();

					// If the csv flight doesn't exist in the database then add it to the database and to the list of flights to be returned
					foreach(CsvFlightEntry flight in flights)
					{
						if (!LookForMatchingFlights(flight, dbFlights))
						{
							FlightEntryModel flightEntryModel = FlightEntryToFlightEntryModel(flight);
							if (await _database.SaveFlightAsync(flightEntryModel) == 1) //returns amount of rows added
							{
								flightsToReturn.Add(flightEntryModel);
							}
						}
					}
				}

			}
			
			return flightsToReturn;
		}

		private bool LookForMatchingFlights(CsvFlightEntry flightEntry, List<FlightEntryModel> flightEntryModels)
		{
			foreach (var model in flightEntryModels)
			{
				if (flightEntry.DateTime != model.DateTime) continue;
				if (!flightEntry.Site.Equals(model.Site)) continue;
				if (!flightEntry.Glider.Equals(model.Glider)) continue;
				if (flightEntry.FlightCount != model.FlightCount) continue;
				if (flightEntry.Hours != model.Hours) continue;
				if (flightEntry.Minutes != model.Minutes) continue;
				if (flightEntry.OmitFromTotals != model.OmitFromTotals) continue;
				if (!flightEntry.Notes.Equals(model.Notes)) continue;
				return true;
			}
			return false;
		}

		private FlightEntryModel FlightEntryToFlightEntryModel(CsvFlightEntry flightEntry)
		{
			return new FlightEntryModel()
			{
				DateTime = flightEntry.DateTime,
				Site = flightEntry.Site,
				Glider = flightEntry.Glider,
				FlightCount = flightEntry.FlightCount,
				Hours = flightEntry.Hours,
				Minutes = flightEntry.Minutes,
				OmitFromTotals = flightEntry.OmitFromTotals,
				Notes = flightEntry.Notes,
			};
		}
	}
}
