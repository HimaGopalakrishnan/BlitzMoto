using App.Features.Accessories.Models;
using App.Features.Base.Services;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Features.Accessories.Services
{
    public class AccessoriesService : BaseService, IAccessoriesService
    {
        #region Constructor

        public AccessoriesService()
        {
        }

        #endregion

        #region Methods

        public async Task<List<Accessory>> GetAllAccessories()
        {
            return (await FirebaseClient
              .Child("Accessories")
              .OnceAsync<Accessory>()).Select(item => new Accessory
              {
                  Number = item.Object.Number,
                  Name = item.Object.Name,
                  Quantity = item.Object.Quantity,
                  Price = item.Object.Price

              }).ToList();
        }

        public async Task<bool> AddAccessory(Accessory accessory)
        {
            await FirebaseClient
               .Child("Accessories")
               .PostAsync(accessory);
            return true;
        }

        public async Task<Accessory> GetAccessory(int id)
        {
            var allAccessories = await GetAllAccessories();
            await FirebaseClient
              .Child("Accessories")
              .OnceAsync<Accessory>();
            return allAccessories.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<bool> UpdateAccessory(Accessory accessory)
        {
            var toUpdateAccessory = (await FirebaseClient
              .Child("Accessories")
              .OnceAsync<Accessory>()).Where(x => x.Object.Id == accessory.Id).FirstOrDefault();

            await FirebaseClient
              .Child("Accessories")
              .Child(toUpdateAccessory.Key)
              .PutAsync(accessory);
            return true;
        }

        public async Task<bool> DeleteAccessory(int id)
        {
            var toDeleteAccessory = (await FirebaseClient
              .Child("Accessories")
              .OnceAsync<Accessory>()).Where(x => x.Object.Id == id).FirstOrDefault();
            await FirebaseClient.Child("Accessories").Child(toDeleteAccessory.Key).DeleteAsync();
            return true;
        }

        #endregion
    }
}
