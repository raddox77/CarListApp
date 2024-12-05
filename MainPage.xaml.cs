using CarListApp.maui.ViewModels;

namespace CarListApp.maui;

public partial class MainPage : ContentPage
{

	public MainPage(CarListViewModel carListViewModel)
	{
		InitializeComponent();
		BindingContext = carListViewModel;
	}
}

