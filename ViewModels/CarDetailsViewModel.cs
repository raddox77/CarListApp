using System;
using CarListApp.maui.Models;
using CarListApp.maui.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Web;

namespace CarListApp.maui.ViewModels;

[QueryProperty(nameof(Id), "Id")]
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
        if (query.TryGetValue("Id", out var idValued) && int.TryParse(idValued?.ToString(), out var id) && id > 0)
        {
            Id = id;
            Car = App.CarService.GetCar(Id);
            if (Car == null)
            {
                await Shell.Current.DisplayAlert("No Recod Found", $"No records Found for Id: {Id}", "Ok");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Invalid Query", $"The Id parameter [{Id}] is missing or invalid", "Ok");
        }
    }
}
