using System;
using CarListApp.maui.Models;

namespace CarListApp.maui.Services;

public class CarService
{
    public List<Car> GetCars()
    {
        return new List<Car>()
        {
            new Car
            {
                Id = 1, Make = "Honda", Model = "Fit", Vin="123"
            },
            new Car
            {
                Id = 2, Make = "Jeep", Model = "Grand Cherokee", Vin="123"
            },
            new Car
            {
                Id = 3, Make = "Range Rover", Model = "LandRover", Vin="123"
            },
            new Car
            {
                Id = 4, Make = "Dodge", Model = "TRX", Vin="123"
            },
            new Car
            {
                Id = 5, Make = "Ford", Model = "Raptor", Vin="123"
            },
            new Car
            {
                Id = 6, Make = "Mercedes Benz", Model = "S500", Vin="123"
            },
            new Car
            {
                Id = 7, Make = "BMW", Model = "M5", Vin="123"
            }

        };
    }


}
