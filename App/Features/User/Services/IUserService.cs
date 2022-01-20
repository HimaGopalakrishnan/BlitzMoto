using System.Threading.Tasks;
using App.Features.User.Models;

namespace App.Features.User.Services
{
    public interface IUserService
    {
        bool IsSignIn();
        Task<bool> CreateUser(UserModel user);
        Task<string> SignIn(UserModel user);
        void SignOut();
        Task ResetPassword(string email);
    }
}
