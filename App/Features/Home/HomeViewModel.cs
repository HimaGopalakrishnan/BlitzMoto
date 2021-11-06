using System.Threading.Tasks;
using System.Windows.Input;
using App.Constants;
using App.Features.Accessories.Pages.List;
using App.Features.SpareParts.Pages.List;
using App.Providers.Navigation.Services;
using Xamarin.Forms;

namespace App.Features.Home
{
    public class HomeViewModel
    {
        #region Services

        readonly INavigationService _navigationService;

        #endregion

        #region Commands

        public ICommand NavigationCommand { get; set; }

        #endregion

        #region Constructor

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            }
        }

        #endregion
    }
}
