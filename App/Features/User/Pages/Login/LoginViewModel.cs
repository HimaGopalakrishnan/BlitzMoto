using App.Constants;
using App.Features.Menu.Pages;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using App.Providers.Navigation.Services;
using App.Providers.Validation;
using App.Resx;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using App.Providers.Validation.Rules;
using App.Features.User.Models;

namespace App.Features.User.Pages.Login
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

        ValidatableObject<string> _mobile;
        public ValidatableObject<string> Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }

        ValidatableObject<string> _password;
        public ValidatableObject<string> Password
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

        public LoginViewModel()
        {
            _navigationService = ViewModelLocator.Resolve<INavigationService>();
            _dialogService = ViewModelLocator.Resolve<IDialogService>();

            LoginCommand = new Command(async () => await Login());
            EyeImageClickedCommand = new Command(EyeImageClicked);

            Mobile = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            AddValidations();
        }

        #endregion

        #region Methods

        void EyeImageClicked()
        {
            IsPassword = !IsPassword;
        }

        async Task Login()
        {
            bool isValid = Validate();
            if (isValid)
            {
                var user = new UserModel { Mobile = Mobile.Value, Password = Password.Value };
                await ApiCallManager.ExecuteCall(() => UserService.SignIn(user),
                    username =>
                    {
                        if (!string.IsNullOrEmpty(username))
                        {
                            Preferences.Set(PreferenceConstants.IsAdmin, username.Equals(UserDetails.AdminUsername));
                            Preferences.Set(PreferenceConstants.Mobile, Mobile.Value.Trim());
                            Preferences.Set(PreferenceConstants.Username, username);
                            Preferences.Set(PreferenceConstants.Password, Password.Value.Trim());
                            Application.Current.MainPage = new NavigationPage(new MenuView());
                        }
                        else
                        {
                            _dialogService.Alert(AppResources.Message_Login_Failed);
                        }
                    },
                    error =>
                    {
                        _dialogService.Alert(AppResources.Message_Login_Failed);

                    }, showBusy: true, busyMessage: AppResources.Loading, ignoreCache: true);
            }
        }

        public bool Validate()
        {
            ValidateEmail();
            ValidatePassword();

            return _mobile.IsValid && _password.IsValid;
        }

        bool ValidateEmail()
        {
            return _mobile.Validate();
        }

        bool ValidatePassword()
        {
            return _password.Validate();
        }

        void AddValidations()
        {
            _mobile.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Message_Enter_Contact_Number });
            _mobile.Validations.Add(new IsValidContactNumberRule<string> { ValidationMessage = AppResources.Message_Enter_Valid_Contact_Number });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Message_Enter_Password });
        }

        #endregion
    }
}