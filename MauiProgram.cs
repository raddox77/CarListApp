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

  // Setup API services
		//builder.Services.AddTransient<CarApiService>();
		builder.Services.AddHttpClient<CarApiService>(client => 
		{
			client.BaseAddress = new Uri(CarApiService.BaseAddress);
		});

  // ViewModels
		builder.Services.AddSingleton<CarListViewModel>();
		builder.Services.AddTransient<CarDetailsViewModel>();
		builder.Services.AddTransient<LoadingPageViewModel>();
		builder.Services.AddTransient<LoginViewModel>();

  // Views or pages
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<LoadingPage>();
		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddTransient<CarDetailsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
