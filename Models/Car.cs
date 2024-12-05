using System;

namespace CarListApp.maui.Models;
    public class Car : BaseEntity
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;  
        public string Vin { get; set; } = null!;
    }