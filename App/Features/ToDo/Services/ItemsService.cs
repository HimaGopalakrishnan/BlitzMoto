using System.Collections.Generic;
using System.Threading.Tasks;
using App.Features.ToDo.Models;
using App.Features.ToDo.Services;

namespace App.Features.List.Services
{
    public class ItemsService : IItemsService
    {
        readonly IItemsApi _apiClient;

        public ItemsService(IItemsApi apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            var response = await _apiClient.GetItems();
            return response;
        }

        public async Task<Item> GetItemDetails(int id)
        {
            var response = await _apiClient.GetItemDetails(id);
            return response;
        }
    }
}
