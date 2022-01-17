using Newtonsoft.Json;
using SQLite;

namespace App.Providers.Database.Models
{
    public class BaseModel
    {
        [JsonIgnore, PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
