using CarListApp.maui.Services;

namespace CarListApp.maui;

public partial class App : Application
{

	public static CarService CarService { get; private set; }
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