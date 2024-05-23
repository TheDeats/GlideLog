using GlideLog.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlideLog.Data
{
	public class FlightDatabase
	{
		SQLiteAsyncConnection Database;

        public FlightDatabase()
        {
			Task[] tasks = [Task.Factory.StartNew(Init)];
            Task.Factory.ContinueWhenAll(tasks, x => x);
        }

		async Task Init()
		{
			if (Database is not null)
				return;

			Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
			var result = await Database.CreateTableAsync<FlightEntryModel>();
		}

		public async Task<List<FlightEntryModel>> GetFlightsAsync()
		{
			//await Init();
			return await Database.Table<FlightEntryModel>().ToListAsync();
		}

		public async Task<FlightEntryModel> GetFlightAsync(int id)
		{
			//await Init();
			return await Database.Table<FlightEntryModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
		}

		public async Task<int> SaveFlightAsync(FlightEntryModel flightEntry)
		{
			//await Init();
			if (flightEntry.ID != 0)
			{
				return await Database.UpdateAsync(flightEntry);
			}
			else
			{
				return await Database.InsertAsync(flightEntry);
			}
		}

		public async Task<int> DeleteFlightAsync(FlightEntryModel flightEntry)
		{
			//await Init();
			return await Database.DeleteAsync(flightEntry);
		}
	}
}
