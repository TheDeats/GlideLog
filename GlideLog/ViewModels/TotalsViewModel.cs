using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GlideLog.Models;

namespace GlideLog.ViewModels
{
	public partial class TotalsViewModel : ObservableObject
	{
		private TotalsModel _totalsModel;
		private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

		[ObservableProperty]
		int flightCount;

		[ObservableProperty]
		int hours;

		[ObservableProperty]
		int minutes;

		public TotalsViewModel(TotalsModel totalsModel)
		{
			_totalsModel = totalsModel;
		}

		[RelayCommand]
		public async Task Appearing()
		{
			try
			{
				await _totalsModel.GetFlightEntriesAsync();
				FlightCount = await _totalsModel.GetTotalFlightsAsync();
				Tuple<int, int> time = await _totalsModel.GetTotalFlightsHoursAsync();
				Hours = time.Item1;
				Minutes = time.Item2;
			}
			catch (Exception ex)
			{
				string message = $"Failed To Load Flights From the Database: {ex.Message}";
				var toast = Toast.Make(message);
				await toast.Show(_cancellationTokenSource.Token);
			}
		}

	}
}
