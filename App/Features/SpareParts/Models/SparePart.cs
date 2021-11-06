using App.Providers.Database.Models;

namespace App.Features.SpareParts.Models
{
    public class SparePart : BaseModel
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
