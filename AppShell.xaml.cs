using CarListApp.maui.Views;

namespace CarListApp.maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(CarDetailsPage),typeof(CarDetailsPage));
	}
}
