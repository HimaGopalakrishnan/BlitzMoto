using System.Threading.Tasks;

namespace App.Features.User.Services
{
    public interface ITokenManager
    {
        Task<bool> HasToken();
        Task<string> GetToken();
        Task SetToken(string token);
        Task<bool> IsValid();
        void ClearToken();
    }
}
