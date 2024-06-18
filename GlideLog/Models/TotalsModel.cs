using GlideLog.Data;

namespace GlideLog.Models
{
	public class TotalsModel
	{
		private FlightDatabase _database;
		private List<FlightEntryModel>? _flightEntries;

		public TotalsModel(FlightDatabase database)
        {
			_database = database;
		}

		public async Task GetFlightEntriesAsync()
		{
			_flightEntries = await _database.GetFlightsAsync();
		}

		public async Task<int> GetTotalFlightsAsync()
		{
			int flightCount = 0;
			await Task.Run(() => 
			{
				foreach (FlightEntryModel flight in _flightEntries!)
				{
					if (!flight.OmitFromTotals)
					{
						flightCount += flight.FlightCount;
					}
				}
			});
			
			return flightCount;
		}

		public async Task<Tuple<int,int>> GetTotalFlightsHoursAsync()
		{
			int hours = 0;
			int minutes = 0;
			await Task.Run(() =>
			{
				foreach (FlightEntryModel flight in _flightEntries!)
				{
					if (!flight.OmitFromTotals)
					{
						minutes += flight.Minutes;
						if(minutes > 59)
						{
							hours++;
							minutes -= 60;
						}
						hours += flight.Hours;
					}
				}
			});

			return new Tuple<int, int> ( hours, minutes );
		}
	}
}
