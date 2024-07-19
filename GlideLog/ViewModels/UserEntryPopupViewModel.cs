using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GlideLog.ViewModels
{
	public partial class UserEntryPopupViewModel : ObservableObject
	{
		//private NewSitePopupModel _newSitePopupModel;
		private readonly IPopupService _popupService;

		public enum EntryPopupType
		{
			Site,
			Glider
		}

		[ObservableProperty]
		string entryLabel = string.Empty;

		[ObservableProperty]
		string userText = string.Empty;

		public UserEntryPopupViewModel(IPopupService popupService)
		{
			//_newSitePopupModel = newSitePopupModel;
			_popupService = popupService;
		}

	}
}
