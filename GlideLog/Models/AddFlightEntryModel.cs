using GlideLog.Data;

namespace GlideLog.Models
{
	public class AddFlightEntryModel
	{
		private FlightDatabase _database;

        public AddFlightEntryModel(FlightDatabase flightDatabase)
        {
			_database = flightDatabase;
		}

		public async Task<bool> AddFlightEntry(FlightEntryModel flightEntryModel)
		{
			try
			{
				if(!(await _database.GetFlightsAsync()).Contains(flightEntryModel))
				{
					if (await _database.SaveFlightAsync(flightEntryModel) == 1) //returns amount of rows added
					{
						return true;
					}
				}
			}
			catch { }
			return false;
		}
    }
}
