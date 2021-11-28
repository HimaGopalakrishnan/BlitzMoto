using App.Features.SpareParts.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Features.SpareParts.Services
{
    public class SpareService : ISpareService
    {
        #region Properties

        FirebaseClient firebaseClient = new FirebaseClient("https://blitzmoto-aee0e-default-rtdb.firebaseio.com/");

        #endregion

        #region Constructor

        public SpareService()
        {
        }

        #endregion

        #region Methods

        public async Task<List<SparePart>> GetAllSpares()
        {
            return (await firebaseClient
              .Child("Spares")
              .OnceAsync<SparePart>()).Select(item => new SparePart
              {
                  Number = item.Object.Number,
                  Name = item.Object.Name,
                  Quantity = item.Object.Quantity,
                  Price = item.Object.Price

              }).ToList();
        }

        public async Task<bool> AddSpare(SparePart spare)
        {
            await firebaseClient
              .Child("Spares")
              .PostAsync(spare);
            return true;
        }

        public async Task<SparePart> GetSpare(int id)
        {
            var allSpares = await GetAllSpares();
            await firebaseClient
              .Child("Spares")
              .OnceAsync<SparePart>();
            return allSpares.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task UpdateSpare(SparePart spare)
        {
            var toUpdateSpare = (await firebaseClient
              .Child("Spares")
              .OnceAsync<SparePart>()).Where(x => x.Object.Id == spare.Id).FirstOrDefault();

            await firebaseClient
              .Child("Spares")
              .Child(toUpdateSpare.Key)
              .PutAsync(spare);
        }

        public async Task DeleteSpare(int id)
        {
            var toDeleteSpare = (await firebaseClient
              .Child("Spares")
              .OnceAsync<SparePart>()).Where(x => x.Object.Id == id).FirstOrDefault();
            await firebaseClient.Child("Spares").Child(toDeleteSpare.Key).DeleteAsync();
        }

        #endregion
    }
}