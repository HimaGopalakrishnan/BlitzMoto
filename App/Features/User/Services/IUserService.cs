using System.Threading.Tasks;
using App.Features.User.Models;

namespace App.Features.User.Services
{
    public interface IUserService
    {
        Task<LoginResponseModel> Register(LoginRequestModel model);
        LoginResponseModel Login(LoginRequestModel model);
    }
}
