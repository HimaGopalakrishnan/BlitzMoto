using App.Providers.Database.Models;

namespace App.Features.Accessories.Models
{
    public class Accessory : BaseModel
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
