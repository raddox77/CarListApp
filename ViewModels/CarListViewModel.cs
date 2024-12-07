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
    const string editButtonText = "Update Car";
    const string createButtonText = "Add Car";
    public ObservableCollection<Car> Cars { get; private set;} = new ();

    public CarListViewModel(CarService CarService)
    {
        Title = "Car List";
        AddEditButtonText = createButtonText;
        //GetCarList.Wait();
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await GetCarList();
    }

    [ObservableProperty]
    bool isRefreshing;
    [ObservableProperty]
    string make;
    [ObservableProperty]
    string model;
    [ObservableProperty]
    string vin;
    [ObservableProperty]
    string addEditButtonText;
    [ObservableProperty]
    int carId;

    [RelayCommand]
    async Task GetCarList()
    {
        if (IsLoading) return;
        try
        {
            IsLoading = true;
            if(Cars.Any()) Cars.Clear();

            var cars = App.CarService.GetCars();
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
    async Task GetCarDetails(int id)
    {
        if(id == 0) 
        {
            await Shell.Current.DisplayAlert("Error","ID was not passed", "Ok");
            return;
        }
        else
        {
            try
            {
                //await Shell.Current.DisplayAlert("Info",$"ID is [{id}]", "Ok");
                await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"{ex.Message} : " + id );
            }
        }
    }

    [RelayCommand]
    async Task SaveCar()
    {
        if(string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
        {
            await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "ok");
            return;
        }
        var car = new Car
        {
            Make = Make,
            Model = Model,
            Vin = Vin
        };

        if (CarId != 0 )
        {
            car.Id = CarId;
            App.CarService.UpdateCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
        }
        else
        {
            App.CarService.AddCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
        }
        await GetCarList();
        await ClearForm();
    }

    [RelayCommand]
    async Task DeleteCar(int id)
    {
        if (id==0)
        {
            await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
            return;
        }
        var result = App.CarService.DeleteCar(id);
        if(result == 0) await Shell.Current.DisplayAlert("Operation Failed", "Pease insert valid data", "Ok");
        else
        {
            await Shell.Current.DisplayAlert("Deltion Successful", "Reord Removed Successfully", "Ok");
            await GetCarList();
        }

    }

    [RelayCommand]
    async void UpdateCar()
    {
        if(string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
        {
            await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "ok");
            return;
        }
        var car = new Car
        {
            Make = Make,
            Model = Model,
            Vin = Vin
        };

        App.CarService.UpdateCar(car);
        await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
        await GetCarList();
    }

    // 
    [RelayCommand]
    async Task SetEditMode(int id)
    {
        AddEditButtonText = editButtonText;
        CarId = id;
        var car = App.CarService.GetCar(id);
        Make = car.Make;
        Model = car.Model;
        Vin = car.Vin;
    }

    [RelayCommand]
    async Task ClearForm()
    {
        AddEditButtonText = createButtonText;
        CarId = 0;
        Make = string.Empty;
        Model = string.Empty;
        Vin = string.Empty;
    }
}
