using System;
using CarListApp.maui.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarListApp.maui.ViewModels;

[QueryProperty(nameof(Car), "Car")]
public partial class CarDetailsViewModel : BaseViewModel
{
    [ObservableProperty]

    #pragma warning disable CS8618
    Car? car;
    #pragma warning restore CS8618
}
