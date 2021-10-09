using System.Collections.Generic;
using System.Threading.Tasks;
using App.Features.ToDo.Models;

namespace App.Features.List.Services
{
    public class MockItemsService : IItemsService
    {
        public MockItemsService()
        {
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            await Task.Delay(1000);
            return new List<Item>
            {
                new Item{Name="Item1",Country="Country1"},
                new Item{Name="Item2",Country="Country2"},
                new Item{Name="Item3",Country="Country3"},
                new Item{Name="Item4",Country="Country4"},
                new Item{Name="Item5",Country="Country5"},
                new Item{Name="Item6",Country="Country6"}
            };
        }

        public async Task<Item> GetItemDetails(int id)
        {
            await Task.Delay(1000);
            return new Item { Name = "Item", Country = "Country" };
        }
    }
}
