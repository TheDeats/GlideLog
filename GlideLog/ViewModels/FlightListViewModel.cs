using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GlideLog.Data;
using GlideLog.Models;
using GlideLog.Views;
using System.Collections.ObjectModel;

namespace GlideLog.ViewModels
{
    public partial class FlightListViewModel : ObservableObject
    {
        private FlightDatabase _database;

        [ObservableProperty]
        ObservableCollection<FlightEntryModel> flights;

        public FlightListViewModel(FlightDatabase database)
        {
            Flights = new();
            _database = database;
        }

		[RelayCommand]
		async Task AddFlight()
        {
            await Shell.Current.GoToAsync(nameof(AddFlightEntryView));
        }

        [RelayCommand]
        public void Appearing()
        {
            try
            {
                // Get the flights from the database
                List<FlightEntryModel> myFlights = new List<FlightEntryModel>();
                Task.Run(async () =>
                {
                    myFlights = await _database.GetFlightsAsync();
                }).Wait();

                // Update the UI
                UpdateFlightsCollection(myFlights);
            }
            catch(Exception ex)
            {
				CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
				ToastDuration duration = ToastDuration.Short;
				double fontSize = 14;
				string message = $"Failed To Load Flights From the Database: {ex.Message}";
				var toast = Toast.Make(message, duration, fontSize);
				toast.Show(cancellationTokenSource.Token);
			}
        }

        [RelayCommand]
		async Task DeleteFlight(FlightEntryModel flightEntryModel)
		{
            if (Flights.Contains(flightEntryModel))
            {
                await _database.DeleteFlightAsync(flightEntryModel);
                Flights.Remove(flightEntryModel);
            }
		}

        public void UpdateFlightsCollection(List<FlightEntryModel> flightEntryModels)
        {
            Flights.Clear();
            foreach(FlightEntryModel flight in flightEntryModels)
            {
                Flights.Add(flight);
            }
        }
	}
}
