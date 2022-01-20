using App.Providers.Database.Models;

namespace App.Features.User.Models
{
    public class UserModel : BaseModel
    {
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
    }
}