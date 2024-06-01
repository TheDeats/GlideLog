using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GlideLog.Models;

namespace GlideLog.ViewModels
{
	public partial class AddFlightEntryViewModel : ObservableObject
	{
        private AddFlightEntryModel _addFlightEntryModel;

		[ObservableProperty]
        string date = DateTime.Now.ToString("M/d/yyyy");

        [ObservableProperty]
        TimeSpan time = DateTime.Now.TimeOfDay;

		[ObservableProperty]
        string site = string.Empty;

        [ObservableProperty]
        string glider = string.Empty;

        [ObservableProperty]
        int flightCount = 1;

        [ObservableProperty]
        int hours;

        [ObservableProperty]
        int minutes;

        [ObservableProperty]
        bool omitFromTotals;

        [ObservableProperty]
        string notes = string.Empty;

        public AddFlightEntryViewModel(AddFlightEntryModel addFlightEntryModel)
        {
			_addFlightEntryModel = addFlightEntryModel;
		}

        [RelayCommand]
        public async Task AddFlightEntryAsync()
        {
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			string[] _dateTime = Date.Split(' ');
            if (DateTime.TryParse($"{_dateTime[0]} {Time}", out DateTime result))
            {
                FlightEntryModel flightModel = new FlightEntryModel()
                {
                    DateTime = result,
                    Site = this.Site,
                    Glider = this.Glider,
                    FlightCount = this.FlightCount,
                    Hours = this.Hours,
                    Minutes = this.Minutes,
                    OmitFromTotals = this.OmitFromTotals,
                    Notes = this.Notes
                };
                if(await _addFlightEntryModel.AddFlightEntry(flightModel))
                {
					string message = "Flight Added";
					var toast = Toast.Make(message);
					await toast.Show(cancellationTokenSource.Token);
				}
                else
                {
					string message = "Failed To Add Flight To Database";
					var toast = Toast.Make(message);
					await toast.Show(cancellationTokenSource.Token);
				}
			}
			else
			{
				string message = "Failed To Parse DateTime, Flight Was Not Added";
				var toast = Toast.Make(message);
				await toast.Show(cancellationTokenSource.Token);
			}
			await Shell.Current.GoToAsync("..");
		}
    }
}
