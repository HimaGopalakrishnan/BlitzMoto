using System;
using System.Linq;
using System.Threading.Tasks;
using App.Constants;
using App.Features.Base.Services;
using App.Features.User.Models;
using Firebase.Database.Query;
using Xamarin.Essentials;

namespace App.Features.User.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService()
        {
        }

        public async Task<bool> CreateUser(UserModel user)
        {
            await FirebaseClient
              .Child("Users")
              .PostAsync(user);
            return true;
        }

        public bool IsSignIn()
        {
            return Preferences.Get(PreferenceConstants.IsSignedIn, false);
        }

        public Task ResetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SignIn(UserModel user)
        {
            var _user = (await FirebaseClient
              .Child("Users")
              .OnceAsync<UserModel>()).Where(x => x.Object.Mobile == user.Mobile).FirstOrDefault();
            Preferences.Set(PreferenceConstants.IsSignedIn, _user != null);
            return _user == null ? string.Empty : _user.Object.Username;
        }

        public void SignOut()
        {
            Preferences.Set(PreferenceConstants.IsSignedIn, false);
        }
    }
}
