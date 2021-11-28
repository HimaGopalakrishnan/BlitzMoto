using App.Constants;
using App.Providers.Navigation.Base;
using Xamarin.Essentials;

namespace App.Features.Menu.Pages
{
    public class MenuViewModel : ViewModelBase
    {
        #region Properties

        bool _isOwner;
        public bool IsOwner
        {
            get => _isOwner;
            set => SetProperty(ref _isOwner, value);
        }

        #endregion

        #region Constructor

        public MenuViewModel()
        {
            IsOwner = Preferences.Get(PreferenceConstants.IsOwner, false);
        }

        #endregion
    }
}