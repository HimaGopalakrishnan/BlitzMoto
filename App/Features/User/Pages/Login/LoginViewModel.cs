using App.Features.User.Services;
using App.Providers.Navigation.Services;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using App.Providers.Validation;
using App.Providers.Validation.Rules;
using App.Resx;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using App.Features.User.Models;
using Xamarin.Essentials;
using App.Features.Menu.Pages;
using App.Constants;

namespace App.Features.User.Pages.Login
{
    public class LoginViewModel : ViewModelBase
    {
        #region Services

        readonly IUserService _userService;
        readonly INavigationService _navigationService;
        readonly IDialogService _dialogService;

        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }
        public ICommand EyeImageClickedCommand { get; set; }

        #endregion

        #region Properties

        ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        bool _isPassword;
        public bool IsPassword
        {
            get => _isPassword;
            set => SetProperty(ref _isPassword, value);
        }

        #endregion

        #region Constructor

        public LoginViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _userService = DependencyService.Get<IUserService>();
            _navigationService = navigationService;
            _dialogService = dialogService;

            LoginCommand = new Command(async () => await Login());
            EyeImageClickedCommand = new Command(EyeImageClicked);

            Email = new ValidatableObject<string>();
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
                var model = new LoginRequestModel
                {
                    Email = Email.Value,
                    Password = Password.Value
                };

                var loginResult = _userService.Login(model);
                if (loginResult != null)
                {
                    Preferences.Set(PreferenceConstants.IsOwner, Email.Value.Trim().Equals(UserDetails.AdminEmail));
                    Preferences.Set(PreferenceConstants.IsLoggedIn, true);
                    Preferences.Set(PreferenceConstants.Username, Email.Value.Trim());
                    Preferences.Set(PreferenceConstants.Password, Password.Value.Trim());
                    await _navigationService.NavigateToAsync<MenuViewModel>();
                }
                else
                {
                    _dialogService.Alert(AppResources.Message_Login_Failed);
                }
            }
        }

        public bool Validate()
        {
            ValidateEmail();
            ValidatePassword();

            return _email.IsValid && _password.IsValid;
        }

        bool ValidateEmail()
        {
            return _email.Validate();
        }

        bool ValidatePassword()
        {
            return _password.Validate();
        }

        void AddValidations()
        {
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Message_Enter_Email });
            _email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = AppResources.Message_Enter_Valid_Email });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Message_Enter_Password });
            _password.Validations.Add(new IsValidPasswordRule<string> { ValidationMessage = AppResources.Message_Enter_Password_With_Eight_Characters });
        }

        #endregion

        #region Override Methods

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                Email = new ValidatableObject<string> { Value = navigationData as string };
            }
            return base.InitializeAsync(navigationData);
        }

        #endregion
    }
}