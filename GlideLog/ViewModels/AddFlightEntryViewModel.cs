using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GlideLog.Data;
using GlideLog.Models;
using Microsoft.VisualBasic;
using System.Threading;

namespace GlideLog.ViewModels
{
	public partial class AddFlightEntryViewModel : ObservableObject
	{
        private FlightDatabase _flightDatabase;

		//[ObservableProperty]
		//DateTime? dateNTime = DateTime.Now;

		[ObservableProperty]
        string date = DateTime.Now.ToString("M/d/yyyy");

        [ObservableProperty]
        TimeSpan time = DateTime.Now.TimeOfDay;

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
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			ToastDuration duration = ToastDuration.Short;
			double fontSize = 14;

			string[] _dateTime = Date.Split(' ');
			if (DateTime.TryParse($"{_dateTime[0]} {Time}", out DateTime result))
            {
				FlightEntryModel flightModel = new FlightEntryModel()
				{
					DateNTime = result,
					Site = this.Site,
					Glider = this.Glider,
					FlightCount = this.FlightCount,
					Hours = this.Hours,
					Minutes = this.Minutes,
					OmitFromTotals = this.OmitFromTotals,
					Notes = this.Notes
				};

				if (await _flightDatabase.SaveFlightAsync(flightModel) == 1) //returns amount of rows added
                {
					string message = "Flight Added";
					var toast = Toast.Make(message, duration, fontSize);
					await toast.Show(cancellationTokenSource.Token);
				}
                else
                {
					string message = "Failed To Add Flight To Database";
					var toast = Toast.Make(message, duration, fontSize);
					await toast.Show(cancellationTokenSource.Token);
				}
			}
            else
            {
				string message = "Failed To Parse DateTime, Flight Was Not Added";
				var toast = Toast.Make(message, duration, fontSize);
				await toast.Show(cancellationTokenSource.Token);
			}

			
            await Shell.Current.GoToAsync("..");
        }
    }
}
