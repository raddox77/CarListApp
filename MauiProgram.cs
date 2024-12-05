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



  // Set up SQLite db services
		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CarService>(s, dbPath));

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
