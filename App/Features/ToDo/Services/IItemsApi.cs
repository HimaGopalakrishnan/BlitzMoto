using System.Collections.Generic;
using System.Threading.Tasks;
using App.Features.ToDo.Models;
using Refit;

namespace App.Features.ToDo.Services
{
    public interface IItemsApi
    {
        [Get("/breweries")]
        Task<IEnumerable<Item>> GetItems();

        [Get("/breweries/{id}")]
        Task<Item> GetItemDetails(int id);
    }
}
