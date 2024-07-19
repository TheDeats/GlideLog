using CommunityToolkit.Maui.Views;
using GlideLog.ViewModels;

namespace GlideLog.Views;

public partial class UserEntryPopupView : Popup
{
	private UserEntryPopupViewModel _viewModel;

	public UserEntryPopupView(UserEntryPopupViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

	private async void CancelButton_Clicked(object sender, EventArgs e)
	{
		await CloseAsync(null);
	}

	private async void OKButton_Clicked(object sender, EventArgs e)
	{
		await CloseAsync(_viewModel.UserText);
	}
}