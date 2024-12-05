using CarListApp.maui.Services;
using CarListApp.maui.ViewModels;
using CarListApp.maui.Views;
using Microsoft.Extensions.Logging;

namespace CarListApp.maui;

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
		builder.Services.AddSingleton<CarService>();

  // ViewModels
		builder.Services.AddSingleton<CarListViewModel>();
		builder.Services.AddTransient<CarDetailsViewModel>();

  // Views or pages
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<CarDetailsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
