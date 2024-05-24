using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        string site = string.Empty;

        [ObservableProperty]
        string glider = string.Empty;

        [ObservableProperty]
        int flightCount;

        [ObservableProperty]
        int hours;

        [ObservableProperty]
        int minutes;

        [ObservableProperty]
        bool omitFromTotals;

        [ObservableProperty]
        string notes = string.Empty;

        public AddFlightEntryViewModel(FlightDatabase flightDatabase)
        {
            _flightDatabase = flightDatabase;
        }

        [RelayCommand]
        public async Task AddFlightEntryAsync()
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
            if(await _flightDatabase.SaveFlightAsync(flightModel) == 1) //returns amount of rows added
            {
                // TODO add toast success message
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
