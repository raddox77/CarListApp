using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CarListApp.maui.Models;
using CarListApp.maui.Services;
using CarListApp.maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.maui.ViewModels;

public partial class CarListViewModel : BaseViewModel
{
    private readonly CarService carService;
    public ObservableCollection<Car> Cars { get; private set;} = new ();
    public CarListViewModel(CarService carService)
    {
        Title = "Car List";
        this.carService = carService;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetCarList()
    {
        if (IsLoading) return;
        try
        {
            IsLoading = true;
            if(Cars.Any()) Cars.Clear();

            var cars = carService.GetCars();
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get cars: {ex.Message}");
            await Shell.Current.DisplayAlert("Error", "Failed to retieve list of cars.", "Ok");
        }
        finally
        {
            IsLoading = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GetCarDetails(Car car)
    {
        if(car == null) return;
        await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object> 
        { 
            { nameof(Car), car }
        });
    }
}
