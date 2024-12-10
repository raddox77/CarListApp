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
    private readonly CarApiService carApiService;

    public CarListViewModel(CarApiService carApiService)
    {
        Title = "Car List";
        AddEditButtonText = createButtonText;
        this.carApiService = carApiService;
        //GetCarList.Wait();
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        //await GetCarList();
        await carApiService.GetCars();
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
            //var cars = App.CarService.GetCars();
            var cars = new List<Car>();
            cars = await carApiService.GetCars();
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
                await Shell.Current.DisplayAlert("Info",$"ID is [{id}]", "Ok");
                //await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
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
            //App.CarService.UpdateCar(car);
            await carApiService.UpdateCar(CarId,car);
            await Shell.Current.DisplayAlert("Info", carApiService.StatusMessage, "Ok");
        }
        else
        {
            //App.CarService.AddCar(car);
            await carApiService.AddCar(car);
            await Shell.Current.DisplayAlert("Info", carApiService.StatusMessage, "Ok");
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
        //var result = App.CarService.DeleteCar(id);
        await carApiService.DeleteCar(id);
        if(carApiService.StatusMessage.Equals("Successful"))
        {
            await Shell.Current.DisplayAlert("Delete Was Successful", carApiService.StatusMessage, "Ok");
        }
        else
        {
            await Shell.Current.DisplayAlert("Deltion Failed", "Reord Removed Successfully", "Ok");
        }
        await carApiService.GetCars();
    }

    [RelayCommand]
    async void UpdateCar()
    {
        //await Shell.Current.DisplayAlert("Debug", $"Data [{Make}]:[{Model}]:[{Vin}]", "ok");
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

        //await Shell.Current.DisplayAlert("Debug", $"Calling UpdateCar({car.Id})", "ok");
        //App.CarService.UpdateCar(car);
        await carApiService.UpdateCar(car.Id, car);
        //await Shell.Current.DisplayAlert("Info", carApiService.StatusMessage, "Ok");
        await carApiService.GetCars();
    }

    // 
    [RelayCommand]
    async Task SetEditMode(int id)
    {
        //await Shell.Current.DisplayAlert("Debug", $"Passing [{id}] to SetEditMode", "OK");
        AddEditButtonText = editButtonText;
        CarId = id;
        //var car = App.CarService.GetCar(id);
        //await Shell.Current.DisplayAlert("Debug", $"Calling GetCar({id})", "OK");
        var car = await carApiService.GetCar(id);
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
