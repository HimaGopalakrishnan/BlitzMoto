using App.Features.Vehicles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Features.Vehicles.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehicleDetails();
        Task<bool> SaveVehicleDetails(Vehicle vehicle);
        Task<Vehicle> GetVehicleDetails(string caseNumber);
        Task UpdateVehicleDetails(Vehicle spare);
        Task DeleteVehicleDetails(int id);
    }
}
