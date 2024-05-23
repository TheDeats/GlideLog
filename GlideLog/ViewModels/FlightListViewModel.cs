using GlideLog.Data;
using GlideLog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GlideLog.ViewModels
{
    public class FlightListViewModel
    {
        public FlightDatabase _database;
        public ObservableCollection<FlightEntryModel> Flights { get; set; } = new();

        public ICommand AddFlightCommand { get; private set; }
        public ICommand SelectionChangedCommand {  get; private set; }

        public FlightListViewModel()
        {
            _database = new FlightDatabase();
            SelectionChangedCommand = new Command(OnSelectionChanged);
            AddFlightCommand = new Command(AddFlight);
        }

        public void OnSelectionChanged()
        {

        }

        public void AddFlight()
        {
            // implement navigation
            Shell.Current.GoToAsync("AddOrEditFlightEntryView");
        }
    }
}
