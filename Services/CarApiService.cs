using System;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using CarListApp.maui.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Newtonsoft.Json;

namespace CarListApp.maui.Services;

public class CarApiService
{
    HttpClient _httpClient;
    public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.iOS ? "http://10.211.55.56:80" : "http://localhost:80";
    public string StatusMessage;
    public CarApiService(HttpClient httpClient)
    {
        //_httpClient = new() { BaseAddress = new Uri(BaseAddress)};
        _httpClient = httpClient;
    }

    public async Task<List<Car>> GetCars()
    {
        try
        {
            var response = await _httpClient.GetStringAsync("/cars");
            return JsonConvert.DeserializeObject<List<Car>>(response);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data";
        }
        return null;
    }

    public async Task<Car> GetCar(int id)
    {
        //await Shell.Current.DisplayAlert("Debug",$"Inside of GetCar({id})","Ok");
        try
        {
            //await Shell.Current.DisplayAlert("Debug",$"Calling GetStringAsync(/car/ + {id})","Ok");
            var response = await _httpClient.GetStringAsync("/cars/" + id);
            return JsonConvert.DeserializeObject<Car>(response);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data";
        }
        //await Shell.Current.DisplayAlert("Debug", "Returning NULL","Ok");
        return null;
    }

    public async Task AddCar(Car car)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/cars/", car );
            response.EnsureSuccessStatusCode();
            StatusMessage = "Insert Successful";
        }
        catch (Exception)
        {
            StatusMessage = "Failed to add data";
        }
    }

    public async Task DeleteCar(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync("/cars/" + id );
            response.EnsureSuccessStatusCode();
            StatusMessage = "Successful";
        }
        catch (Exception)
        {
            StatusMessage = "Failed";
        }

    }

    public async Task UpdateCar(int id, Car car)
    {
       try
        {
            await Shell.Current.DisplayAlert("Debug", $"In update for id [{id}]", "ok");
            var response = await _httpClient.PutAsJsonAsync("/cars/" + id, car);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Update Successful";
        }
        catch (Exception)
        {
            StatusMessage = "Failed to update data";
        }
    }

}
