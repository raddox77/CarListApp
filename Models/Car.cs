using System;
using SQLite;

namespace CarListApp.maui.Models;
    
    [Table("cars")]
    public class Car : BaseEntity
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;  
        [MaxLength(12)] 
        [Unique]
        public string Vin { get; set; } = null!;
    }