using System.Collections.Generic;
using System.Threading.Tasks;
using App.Features.Accessories.Models;

namespace App.Features.Accessories.Services
{
    public interface IAccessoriesService
    {
        Task<List<Accessory>> GetAllAccessories();
        Task<bool> AddAccessory(Accessory accessory);
        Task<Accessory> GetAccessory(int id);
        Task<bool> UpdateAccessory(Accessory accessory);
        Task<bool> DeleteAccessory(int id);
    }
}
