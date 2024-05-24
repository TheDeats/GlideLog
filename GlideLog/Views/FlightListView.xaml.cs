using GlideLog.ViewModels;

namespace GlideLog.Views;

public partial class FlightListView : ContentPage
{
	public FlightListView(FlightListViewModel flightListViewModel)
	{
		InitializeComponent();
		BindingContext = flightListViewModel;
	}
}