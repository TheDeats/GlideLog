using GlideLog.ViewModels;

namespace GlideLog.Views;

public partial class AddFlightEntryView : ContentPage
{
	public AddFlightEntryView(AddFlightEntryViewModel addFlightEntryViewModel)
	{
		InitializeComponent();
		BindingContext = addFlightEntryViewModel;
	}
}