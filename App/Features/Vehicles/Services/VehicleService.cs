using App.Features.Base.Services;
using App.Features.Vehicles.Models;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Features.Vehicles.Services
{
    public class VehicleService : BaseService, IVehicleService
    {
        #region Constructor

        public VehicleService()
        {

        }

        #endregion

        #region Methods

        public async Task<List<Vehicle>> GetAllVehicleDetails()
        {
            return (await FirebaseClient
              .Child("Vehicles")
              .OnceAsync<Vehicle>()).Select(item => new Vehicle
              {
                  Name = item.Object.Name,
                  Model = item.Object.Model,
                  CaseNumber = item.Object.CaseNumber,
                  EngineNumber = item.Object.EngineNumber,
                  ServiceDate = item.Object.ServiceDate,
                  NextServiceDate = item.Object.NextServiceDate,
                  SpareUsed = item.Object.SpareUsed,
                  SpareCount = item.Object.SpareCount,
                  Kilometer = item.Object.Kilometer,
                  Note = item.Object.Note,

              }).ToList();
        }

        public async Task<bool> SaveVehicleDetails(Vehicle vehicle)
        {
            await FirebaseClient
              .Child("Vehicles")
              .PostAsync(vehicle);
            return true;
        }

        public async Task<Vehicle> GetVehicleDetails(int id)
        {
            var allSpares = await GetAllVehicleDetails();
            await FirebaseClient
              .Child("Vehicles")
              .OnceAsync<Vehicle>();
            return allSpares.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task UpdateVehicleDetails(Vehicle spare)
        {
            var toUpdateSpare = (await FirebaseClient
              .Child("Vehicles")
              .OnceAsync<Vehicle>()).Where(x => x.Object.Id == spare.Id).FirstOrDefault();

            await FirebaseClient
              .Child("Vehicles")
              .Child(toUpdateSpare.Key)
              .PutAsync(spare);
        }

        public async Task DeleteVehicleDetails(int id)
        {
            var toDeleteSpare = (await FirebaseClient
              .Child("Vehicles")
              .OnceAsync<Vehicle>()).Where(x => x.Object.Id == id).FirstOrDefault();
            await FirebaseClient.Child("Vehicles").Child(toDeleteSpare.Key).DeleteAsync();
        }

        #endregion
    }
}
