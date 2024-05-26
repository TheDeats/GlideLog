using CommunityToolkit.Maui;
using GlideLog.Data;
using GlideLog.Models;
using GlideLog.ViewModels;
using GlideLog.Views;
using Microsoft.Extensions.Logging;

namespace GlideLog
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
                .UseMauiCommunityToolkit(options =>
				{
					options.SetShouldEnableSnackbarOnWindows(true);
				})
                .ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif
			builder.Services.AddSingleton<FlightListView>();
			builder.Services.AddSingleton<FlightListViewModel>();
			builder.Services.AddSingleton<FlightListModel>();

            builder.Services.AddTransient<AddFlightEntryView>();
			builder.Services.AddTransient<AddFlightEntryViewModel>();
			builder.Services.AddTransient<AddFlightEntryModel>();

            builder.Services.AddSingleton<TotalsView>();
			builder.Services.AddSingleton<FlightDatabase>();

			return builder.Build();
		}
	}
}
