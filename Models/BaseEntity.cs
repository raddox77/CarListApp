using System;
using SQLite;

namespace CarListApp.maui.Models;

public abstract class BaseEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set;}
}
