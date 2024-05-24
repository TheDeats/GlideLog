using CommunityToolkit.Mvvm.ComponentModel;
using GlideLog.Data;
using GlideLog.Models;

namespace GlideLog.ViewModels
{
	public partial class AddFlightEntryViewModel : ObservableObject
	{
        private FlightDatabase _flightDatabase;

        [ObservableProperty]
        DateTime dateNTime;

        [ObservableProperty]
        string site;

        [ObservableProperty]
        string glider;

        [ObservableProperty]
        int flightCount;

        [ObservableProperty]
        int hours;

        [ObservableProperty]
        int minutes;

        [ObservableProperty]
        bool omitFromTotals;

        [ObservableProperty]
        string notes;

		public AddFlightEntryViewModel()
        {
            
        }

        public async Task<bool> AddFlightEntryAsync()
        {
            FlightEntryModel flightModel = new FlightEntryModel()
            {
                DateNTime = this.DateNTime,
                Site = this.Site,
                Glider = this.Glider,
                FlightCount = this.FlightCount,
                Hours = this.Hours,
                Minutes = this.Minutes,
                OmitFromTotals = this.OmitFromTotals,
                Notes = this.Notes
            };
            if(await _flightDatabase.SaveFlightAsync(flightModel) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
