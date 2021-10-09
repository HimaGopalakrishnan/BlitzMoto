using System.Collections.Generic;
using System.Threading.Tasks;
using App.Features.ToDo.Models;

namespace App.Features.List.Services
{
    public interface IItemsService
    {
        Task<IEnumerable<Item>> GetItems();
        Task<Item> GetItemDetails(int id);
    }
}
