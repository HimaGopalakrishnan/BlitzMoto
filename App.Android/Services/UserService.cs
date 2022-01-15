using System.Threading.Tasks;
using Android.App;
using Android.Gms.Tasks;
using App.Droid.Services;
using App.Features.User.Models;
using App.Features.User.Services;
using Firebase;
using Firebase.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]
namespace App.Droid.Services
{
    public class UserService : Activity, IUserService, IOnCompleteListener
    {
        #region Services

        public static FirebaseApp app;
        FirebaseAuth auth;

        #endregion

        #region Constructor

        public UserService()
        {
            InitFirebaseAuth();
        }

        #endregion

        #region Methods

        void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
               .SetApplicationId("1:717310474757:android:b942e48873963e6b41ba6c")
               .SetApiKey("AIzaSyAMOhERVNoHzQqkZIbtKKVqDScsAykHHhs")
               .Build();
            if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
            auth = FirebaseAuth.GetInstance(app);
        }

        public LoginResponseModel Login(LoginRequestModel model)
        {
            var task = auth.SignInWithEmailAndPassword(model.Email, model.Password);

            if (task.IsSuccessful)
            {
                return new LoginResponseModel();
            }
            else
            {
                return null;
            }
        }

        public Task<LoginResponseModel> Register(LoginRequestModel model)
        {
            return null;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            
        }

        #endregion
    }
}