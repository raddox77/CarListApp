using System;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;
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
            //await Shell.Current.DisplayAlert("SetAuthToken", "Called SetAuthToken", "OK");
            await SetAuthToken();
            //await Shell.Current.DisplayAlert("SetAuthToken", "SetAuthToken Done", "OK");

            //await Shell.Current.DisplayAlert("GetCars", "Calling API /cars", "OK");
            var response = await _httpClient.GetStringAsync("/cars");
            
            await Shell.Current.DisplayAlert("GetCars", $"Response: {response}", "OK");
            return JsonConvert.DeserializeObject<List<Car>>(response);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to retrieve data: {ex.Message}";   
        }
        return null;
    }

    public async Task<Car> GetCar(int id)
    {
        await SetAuthToken();
        await Shell.Current.DisplayAlert("Debug",$"Inside of GetCar({id})","Ok");
        try
        {
            await Shell.Current.DisplayAlert("Debug",$"Calling GetStringAsync(/car/ + {id})","Ok");
            var response = await _httpClient.GetStringAsync("/cars/" + id);
            await Shell.Current.DisplayAlert("Debug",$"Response: {response}","Ok");
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
        await SetAuthToken();
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
        await SetAuthToken();
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
       await SetAuthToken();
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

    public async Task<AuthResponseModel> Login(LoginModel loginModel)
    { 
        try 
        {
            var response = await _httpClient.PostAsJsonAsync("/login", loginModel);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Login Successful";

            return JsonConvert.DeserializeObject<AuthResponseModel>(await response.Content.ReadAsStringAsync());
        }
        catch (Exception)
        {
            StatusMessage = "Failed to login successfuly";
            return default;
        }
    }

    public async Task SetAuthToken()
    {
        var token = await SecureStorage.GetAsync("Token");
        //await Shell.Current.DisplayAlert("SetAuthToken", token, "OK");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

}
