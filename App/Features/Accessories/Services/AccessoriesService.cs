using App.Features.Accessories.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Features.Accessories.Services
{
    public class AccessoriesService : IAccessoriesService
    {
        #region Properties

        FirebaseClient firebaseClient = new FirebaseClient("https://blitzmoto-aee0e-default-rtdb.firebaseio.com/");

        #endregion

        #region Constructor

        public AccessoriesService()
        {
        }

        #endregion

        #region Methods

        public async Task<List<Accessory>> GetAllAccessories()
        {
            return (await firebaseClient
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
            await firebaseClient
               .Child("Accessories")
               .PostAsync(accessory);
            return true;
        }

        public async Task<Accessory> GetAccessory(int id)
        {
            var allAccessories = await GetAllAccessories();
            await firebaseClient
              .Child("Accessories")
              .OnceAsync<Accessory>();
            return allAccessories.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<bool> UpdateAccessory(Accessory accessory)
        {
            var toUpdateAccessory = (await firebaseClient
              .Child("Accessories")
              .OnceAsync<Accessory>()).Where(x => x.Object.Id == accessory.Id).FirstOrDefault();

            await firebaseClient
              .Child("Accessories")
              .Child(toUpdateAccessory.Key)
              .PutAsync(accessory);
            return true;
        }

        public async Task<bool> DeleteAccessory(int id)
        {
            var toDeleteAccessory = (await firebaseClient
              .Child("Accessories")
              .OnceAsync<Accessory>()).Where(x => x.Object.Id == id).FirstOrDefault();
            await firebaseClient.Child("Accessories").Child(toDeleteAccessory.Key).DeleteAsync();
            return true;
        }

        #endregion
    }
}
