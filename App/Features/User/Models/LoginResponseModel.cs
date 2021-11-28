using Newtonsoft.Json;

namespace App.Features.User.Models
{
    public class LoginResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
