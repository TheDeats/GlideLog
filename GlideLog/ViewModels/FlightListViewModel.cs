using Android.Widget;
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
        private bool _initialLoad = true;

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
                if (_initialLoad)
                {
                    List<FlightEntryModel> myFlights = new List<FlightEntryModel>();
                    Task.Run(async () =>
                    {
                        myFlights = await _database.GetFlightsAsync();
                    }).Wait();
                    UpdateFlightsCollection(myFlights);
                    _initialLoad = false;
                }
            }
            catch(Exception ex)
            {
                // TODO handle db load error
                // display db load error
            }
        }

        [RelayCommand]
		void DeleteFlight(FlightEntryModel flightEntryModel)
		{
            if (Flights.Contains(flightEntryModel))
            {
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
