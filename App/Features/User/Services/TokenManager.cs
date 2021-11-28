using System.Threading.Tasks;
using Xamarin.Essentials;

namespace App.Features.User.Services
{
    public class TokenManager : ITokenManager
    {
        const string AccessToken = "access_token";

        public async Task SetToken(string token)
        {
            await SecureStorage.SetAsync(AccessToken, token);
        }

        public async Task<bool> HasToken()
        {
            var token = await SecureStorage.GetAsync(AccessToken);
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string> GetToken()
        {
            var token = await SecureStorage.GetAsync(AccessToken);
            return token;
        }

        public async Task<bool> IsValid()
        {
            var token = await SecureStorage.GetAsync(AccessToken);

            if (!string.IsNullOrEmpty(token))
            {
                //var jwtToken = new JwtSecurityToken(token);
                //var expiry = jwtToken.ValidTo.ToLocalTime();
                //return expiry >= DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ClearToken()
        {
            SecureStorage.Remove(AccessToken);
        }
    }
}
