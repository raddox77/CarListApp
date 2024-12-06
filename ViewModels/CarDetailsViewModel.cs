using System;
using CarListApp.maui.Models;
using CarListApp.maui.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Web;

namespace CarListApp.maui.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]

    #pragma warning disable CS8618
    Car? car;
    #pragma warning restore CS8618

    [ObservableProperty]
    int id;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        Car = App.CarService.GetCar(Id);
        if (Car == null)
        {
            await Shell.Current.DisplayAlert($"No Records found for {Id}", "Please try again", "Ok");
        }
    }
}
