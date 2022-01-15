using System;
using App.Features.SpareParts.Models;
using App.Providers.Database.Models;

namespace App.Features.Vehicles.Models
{
    public class Vehicle : BaseModel
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string CaseNumber { get; set; }
        public string EngineNumber { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime NextServiceDate { get; set; }
        public SparePart SpareUsed { get; set; }
        public int SpareCount { get; set; }
        public double Kilometer { get; set; }
        public string Note { get; set; }
    }
}
