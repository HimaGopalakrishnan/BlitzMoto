using System.Globalization;
using System.Threading;
using App.Constants;
using App.Features.Menu.Pages;
using App.Features.User.Pages.Login;
using App.Features.User.Services;
using App.Providers.Navigation.Services;
using App.Resx;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace App
{
    public partial class App : Application
    {
        #region Services

        readonly INavigationService _navigationService;

        #endregion

        #region Constructor

        public App()
        {
            InitializeComponent();
            SetCulture();
            Startup.Init();
            _navigationService = ViewModelLocator.Resolve<INavigationService>();
            Microsoft.AppCenter.AppCenter.Start($"{Flag.Android}={AppCenter.AndroidKey};" + $"{Flag.IOS}={AppCenter.IosKey};", typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Crashes));
        }

        #endregion

        #region Override Methods

        protected override async void OnStart()
        {
            base.OnStart();
            await _navigationService.InitializeAsync();
            if (ViewModelLocator.Resolve<IUserService>().IsSignIn())
            {
                MainPage = new NavigationPage(new MenuView());
            }
            else
            {
                MainPage = new NavigationPage(new LoginView());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

        #region Methods

        void SetCulture()
        {
            CultureInfo englishUSCulture = new CultureInfo(Flag.Culture);
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
            AppResources.Culture = englishUSCulture;
            Thread.CurrentThread.CurrentCulture = englishUSCulture;
            Thread.CurrentThread.CurrentUICulture = englishUSCulture;
        }

        #endregion
    }
}
