using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Constants;
using App.Features.Accessories.Pages.List;
using App.Features.SpareParts.Pages.List;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using App.Providers.Navigation.Services;
using App.Resx;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.Features.Home
{
    public class HomeViewModel : ViewModelBase
    {
        #region Services

        readonly INavigationService _navigationService;
        readonly IDialogService _dialogService;

        #endregion

        #region Commands

        public ICommand NavigationCommand { get; set; }

        #endregion

        #region Constructor

        public HomeViewModel()
        {
            _navigationService = ViewModelLocator.Resolve<INavigationService>();
            _dialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationCommand = new Command<string>(async (view) => await NavigateToPage(view));
        }

        #endregion

        #region Methods

        async Task NavigateToPage(string view)
        {
            switch (view)
            {
                case NavigationConstants.Accessories:
                    await _navigationService.NavigateToAsync<AccessoriesListViewModel>();
                    break;
                case NavigationConstants.Gears:
                    break;
                case NavigationConstants.PowerParts:
                    break;
                case NavigationConstants.Apparels:
                    break;
                case NavigationConstants.Service:
                    break;
                case NavigationConstants.Spare:
                    await _navigationService.NavigateToAsync<SpareListViewModel>();
                    break;
                case NavigationConstants.Whatsapp:
                    try
                    {
                        await Launcher.OpenAsync(new Uri("whatsapp://send?phone=+919747609665"));
                    }
                    catch (Exception ex)
                    {
                        _dialogService.Toast(ex.Message);
                    }
                    break;
                case NavigationConstants.Instagram:
                    try
                    {
                        await Launcher.OpenAsync($"instagram://user?username=Teamblitzmoto");
                    }
                    catch (Exception ex)
                    {
                        _dialogService.Toast(ex.Message);
                    }
                    break;
                case NavigationConstants.Map:
                    try
                    {
                        await Map.OpenAsync(10.883341342145767, 76.07814507948123, new MapLaunchOptions { Name = "BLITZ Moto" });
                    }
                    catch (Exception ex)
                    {
                        _dialogService.Toast(ex.Message);
                    }
                    break;
                case NavigationConstants.Facebook:
                    try
                    {
                        await Launcher.OpenAsync($"fb://");
                    }
                    catch (Exception ex)
                    {
                        _dialogService.Toast(ex.Message);
                    }
                    break;
                case NavigationConstants.Call:
                    try
                    {
                        PhoneDialer.Open("+919747609665");
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                        _dialogService.Toast(ex.Message);
                    }
                    catch (Exception)
                    {
                        _dialogService.Toast(AppResources.SomethingWronMessage);
                    }
                    break;
                case NavigationConstants.Mail:
                    await SendMail();
                    break;
            }
        }

        async Task SendMail()
        {
            try
            {
                string body = $"Hello Blitz team, \n\n "; //+
                        //$"I'm interested in your work and commitment. \n\n" +
                        //$"I really appreciate your knowlodge and hardwork and like to share my feedback with you.\n" +
                        //$"Hope you will continue to serve me like this.\n" +
                        //$"Heartly congratulations and thank you for your service.";

                string footer = $"\n\n Thanks! \n\n";

                body += footer;

                var message = new EmailMessage
                {
                    To = new List<string> { "teamblitzmoto@gmail.com" },
                    Subject = "Feedback",
                    Body = body,
                };

                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                _dialogService.Toast(ex.Message);
            }
            catch (Exception)
            {
                _dialogService.Toast(AppResources.SomethingWronMessage);
            }
        }

        #endregion
    }
}
