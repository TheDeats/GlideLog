﻿using GlideLog.ViewModels;
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
            builder.Services.AddTransient<AddFlightEntryView>();
			builder.Services.AddTransient<AddFlightEntryViewModel>();
            builder.Services.AddSingleton<TotalsView>();

			return builder.Build();
		}
	}
}
