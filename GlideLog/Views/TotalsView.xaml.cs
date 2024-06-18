using GlideLog.ViewModels;

namespace GlideLog.Views;

public partial class TotalsView : ContentPage
{
	public TotalsView(TotalsViewModel totalsViewModel)
	{
		InitializeComponent();
		BindingContext = totalsViewModel;
	}
}