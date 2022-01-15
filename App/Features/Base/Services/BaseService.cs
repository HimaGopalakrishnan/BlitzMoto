using Firebase.Database;

namespace App.Features.Base.Services
{
    public class BaseService
    {
        #region Properties

        public FirebaseClient FirebaseClient = new FirebaseClient("https://blitzmoto-aee0e-default-rtdb.firebaseio.com/");

        #endregion
    }
}
