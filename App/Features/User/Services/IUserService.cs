using System.Threading.Tasks;

namespace App.Features.User.Services
{
    public interface IUserService
    {
        bool IsSignIn();
        Task<bool> CreateUser(string username, string email, string password);
        Task<string> SignIn(string email, string password);
        void SignOut();
        Task ResetPassword(string email);
    }
}
