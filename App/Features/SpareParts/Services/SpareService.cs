using App.Features.Base.Services;
using App.Features.SpareParts.Models;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Features.SpareParts.Services
{
    public class SpareService : BaseService, ISpareService
    {
        #region Constructor

        public SpareService()
        {
        }

        #endregion

        #region Methods

        public async Task<List<SparePart>> GetAllSpares()
        {
            return (await FirebaseClient
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
            await FirebaseClient
              .Child("Spares")
              .PostAsync(spare);
            return true;
        }

        public async Task<SparePart> GetSpare(int id)
        {
            var allSpares = await GetAllSpares();
            await FirebaseClient
              .Child("Spares")
              .OnceAsync<SparePart>();
            return allSpares.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<bool> UpdateSpare(SparePart spare)
        {
            var toUpdateSpare = (await FirebaseClient
              .Child("Spares")
              .OnceAsync<SparePart>()).Where(x => x.Object.Id == spare.Id).FirstOrDefault();

            await FirebaseClient
              .Child("Spares")
              .Child(toUpdateSpare.Key)
              .PutAsync(spare);
            return true;
        }

        public async Task<bool> DeleteSpare(int id)
        {
            var toDeleteSpare = (await FirebaseClient
              .Child("Spares")
              .OnceAsync<SparePart>()).Where(x => x.Object.Id == id).FirstOrDefault();
            await FirebaseClient.Child("Spares").Child(toDeleteSpare.Key).DeleteAsync();
            return true;
        }

        #endregion
    }
}