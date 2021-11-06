using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using App.Providers.Navigation.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.User.Pages
{
    public class LoginViewModel : ViewModelBase
    {
        #region Services

        readonly INavigationService _navigationService;
        readonly IDialogService _dialogService;

        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }
        public ICommand EyeImageClickedCommand { get; set; }

        #endregion

        #region Properties

        string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        bool _isPassword = true;
        public bool IsPassword
        {
            get => _isPassword;
            set => SetProperty(ref _isPassword, value);
        }

        #endregion

        #region Constructor

        public LoginViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            LoginCommand = new Command(async () => await Login());
            EyeImageClickedCommand = new Command(EyeImageClicked);
        }

        #endregion

        #region Methods

        void EyeImageClicked()
        {
            IsPassword = !IsPassword;
        }

        async Task Login()
        {
            await Task.Delay(1);
            bool isValid = Validate();
            if (isValid)
            {
                //add api call here
            }
            else
            {
                _dialogService.Alert("Login Failed");
            }
        }

        public bool Validate()
        {
            return true;
        }

        #endregion
    }
}
