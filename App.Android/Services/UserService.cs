using System.Threading.Tasks;
using App.Droid.Services;
using App.Features.User.Services;
using Firebase.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]
namespace App.Droid.Services
{
    public class UserService : IUserService
    {
        public bool IsSignIn()
            => FirebaseAuth.Instance.CurrentUser != null;

        public async Task<bool> CreateUser(string username, string email, string password)
        {
            var authResult = await FirebaseAuth.Instance
                    .CreateUserWithEmailAndPasswordAsync(email, password);

            var userProfileChangeRequestBuilder = new UserProfileChangeRequest.Builder();
            userProfileChangeRequestBuilder.SetDisplayName(username);

            var userProfileChangeRequest = userProfileChangeRequestBuilder.Build();
            await authResult.User.UpdateProfileAsync(userProfileChangeRequest);

            return await Task.FromResult(true);
        }

        public async Task<string> SignIn(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var token = await authResult.User.GetIdTokenAsync(false);
            return token.Token;
        }

        public void SignOut()
            => FirebaseAuth.Instance.SignOut();

        public async Task ResetPassword(string email)
            => await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
    }
}