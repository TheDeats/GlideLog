using Android.App;
using Android.Runtime;
using GlideLog.DirectoryManagement;
using GlideLog.Platforms.Android;

namespace GlideLog
{
	[Application]
	public class MainApplication : MauiApplication
	{
		public MainApplication(IntPtr handle, JniHandleOwnership ownership)
			: base(handle, ownership)
		{
			DependencyService.Register<IFilePathService, FilePathService>();
		}

		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
	}
}
