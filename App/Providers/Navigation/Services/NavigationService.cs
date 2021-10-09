using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using App.Provider.Navigation.Services;
using App.Providers.Navigation.Base;
using App.Providers.Navigation.Controls;
using App.Providers.Navigation.Enums;
using Xamarin.Forms;

namespace Provider.Navigation.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
        }
        public Task InitializeAsync()
        {
            return Task.FromResult(true);
        }

        public Task NavigateToAsync<TViewModel>(NavigationMode navigationMode = NavigationMode.Push, bool clearNavigationStack = false) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null, navigationMode, clearNavigationStack);
        }

        public Task NavigateToAsync<TViewModel>(object parameter, NavigationMode navigationMode = NavigationMode.Push, bool clearNavigationStack = false) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter, navigationMode, clearNavigationStack);
        }

        public Task NavigateToRootAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.PopToRootAsync();
            }

            return Task.FromResult(true);
        }

        public Task RemoveLastPageAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.PopAsync();
            }

            return Task.FromResult(true);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }
        public async Task RemoveModalAsync()
        {
            var navigationPage = Application.Current.MainPage as CustomNavigationView;
            if (navigationPage != null)
            {
                await navigationPage.Navigation.PopModalAsync();
            }
        }
        public async Task PopAsync()
        {
            var navigationPage = Application.Current.MainPage as CustomNavigationView;
            if (navigationPage != null)
            {
                await navigationPage.Navigation.PopAsync();
            }
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter, NavigationMode navigationMode, bool clearNavigationStack = false)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page != null)
            {
                var pageName = page.GetType().Name;
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("Name", pageName);
                //_analyticsService.TrackEvent(Constants.Analytics.PageVisited, keyValuePairs);
            }

            if (clearNavigationStack)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as CustomNavigationView;
                if (navigationPage != null)
                {
                    if (navigationMode == NavigationMode.Push)
                        await navigationPage.PushAsync(page);
                    if (navigationMode == NavigationMode.Modal)
                        await navigationPage.Navigation.PushModalAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}
