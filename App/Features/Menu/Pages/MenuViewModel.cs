using App.Constants;
using App.Providers.Navigation.Base;
using Xamarin.Essentials;

namespace App.Features.Menu.Pages
{
    public class MenuViewModel : ViewModelBase
    {
        #region Properties

        bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }

        #endregion

        #region Constructor

        public MenuViewModel()
        {
            IsAdmin = Preferences.Get(PreferenceConstants.IsAdmin, false);
        }

        #endregion
    }
}