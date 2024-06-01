using GlideLog.ViewModels;

namespace GlideLog.Views;

public partial class EditFlightEntryView : ContentPage
{
	public EditFlightEntryView(EditFlightEntryViewModel editFlightEntryViewModel)
	{
		InitializeComponent();
		BindingContext = editFlightEntryViewModel;
	}
}