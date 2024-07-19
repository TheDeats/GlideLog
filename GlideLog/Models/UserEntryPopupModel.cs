using GlideLog.Data;

namespace GlideLog.Models
{
    public class UserEntryPopupModel
    {
		private FlightDatabase _database;

        public UserEntryPopupModel(FlightDatabase database)
        {
			_database = database;
		}
    }
}
