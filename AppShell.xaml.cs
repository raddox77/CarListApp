﻿using CarListApp.maui.Views;

namespace CarListApp.maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register navigation routes
		Routing.RegisterRoute(nameof(CarDetailsPage),typeof(CarDetailsPage));
		Routing.RegisterRoute(nameof(LoginPage),typeof(LoginPage));
		Routing.RegisterRoute(nameof(MainPage),typeof(MainPage));
	}
}
