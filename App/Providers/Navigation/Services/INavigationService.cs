using App.Providers.Navigation.Base;
using App.Providers.Navigation.Enums;
using System.Threading.Tasks;

namespace App.Providers.Navigation.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>(NavigationMode navigationMode = NavigationMode.Push, bool clearNavigationStack = false) where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter, NavigationMode navigationMode = NavigationMode.Push, bool clearNavigationStack = false) where TViewModel : ViewModelBase;
        Task NavigateToRootAsync();
        Task RemoveLastPageAsync();
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
        Task RemoveModalAsync();
        Task PopAsync();
    }
}
