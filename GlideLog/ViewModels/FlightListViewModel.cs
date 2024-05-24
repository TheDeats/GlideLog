using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GlideLog.Data;
using GlideLog.Models;
using GlideLog.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GlideLog.ViewModels
{
    public partial class FlightListViewModel : ObservableObject
    {
        public FlightDatabase _database;

        [ObservableProperty]
        ObservableCollection<FlightEntryModel> flights;

        public ICommand SelectionChangedCommand {  get; private set; }

        public FlightListViewModel()
        {
            Flights = new();
            _database = new FlightDatabase();
            SelectionChangedCommand = new Command(OnSelectionChanged);
        }

        public void OnSelectionChanged()
        {

        }

		[RelayCommand]
		async Task AddFlight()
        {
            await Shell.Current.GoToAsync(nameof(AddFlightEntryView));
        }

		[RelayCommand]
		void DeleteFlight(FlightEntryModel flightEntryModel)
		{
            if (Flights.Contains(flightEntryModel))
            {
                Flights.Remove(flightEntryModel);
            }
		}
	}
}
