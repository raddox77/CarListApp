using CarListApp.maui.Services;
using CarListApp.maui.Models;

namespace CarListApp.maui;

public partial class App : Application
{
	public static UserInfo? UserInfo;

	public static CarService? CarService { get; private set; }
	public App(CarService carService)
	{
		InitializeComponent();
		CarService = carService;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}