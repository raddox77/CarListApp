using System;
using System.IO.Pipelines;
using CarListApp.maui.Models;
using SQLite;

namespace CarListApp.maui.Services;

public class CarService
{
    private SQLiteConnection conn = null!;
    string _dbPath;
    public string StatusMessage = null!;

    public CarService(string dbPath)
    {
        _dbPath = dbPath;
    }

    private void Init()
    {
        if (conn != null)
        {
            return;
        }

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Car>();
    }

    public List<Car> GetCars()
    {
        try
        {
            Init(); //Connect to DB
            return conn.Table<Car>().ToList();
        }
        catch (Exception)
        {
            StatusMessage = "Failed to Fetch data";
        }
        return new List<Car>();
    }

    public Car GetCar(int id)
    {
        try
        {
            Init();
            return conn.Table<Car>().FirstOrDefault(q => q.Id == id);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data";
        }
        return null;
    }

    public void AddCar(Car car)
    {
        try{
            Init(); // Connect to DB

            if(car == null)
            {
                throw new Exception("Invalid Car Record");
            }
            var result = conn.Insert(car);
            StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
        }
        catch(Exception)
        {
            StatusMessage = "Failed to Insert data.";
        }
    }

    public int DeleteCar(int id)
    {
        try
        {
            Init(); // Connect to DB
            return conn.Table<Car>().Delete(q => q.Id == id);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to Delete Car";
        }
        return 0;
    }

    public void UpdateCar(Car car)
    {
        try
        {
            Init(); // Connect to DB
            if(car == null)
            {
                throw new Exception("Invalid Car Record");
            }
            var result = conn.Update(car);
            StatusMessage = result == 0 ? "Update Failed" : "Update Succeeded";

        }
        catch(Exception)    
        {
            StatusMessage = "Failed to Update data.";
        }
    }
}
