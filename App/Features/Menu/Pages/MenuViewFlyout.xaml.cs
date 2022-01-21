using App.Constants;
using App.Features.About.Pages;
using App.Features.Accessories.Pages.List;
using App.Features.Home;
using App.Features.SpareParts.Pages.List;
using App.Features.User.Pages.Login;
using App.Features.User.Pages.Register;
using App.Features.User.Services;
using App.Features.Vehicles.Pages.Detail;
using App.Features.Vehicles.Pages.List;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Features.Menu.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuViewFlyout : ContentPage
    {
        public ListView ListView;

        public MenuViewFlyout()
        {
            InitializeComponent();

            BindingContext = new MenuViewFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MenuViewFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuViewFlyoutMenuItem> MenuItems { get; set; }

            public MenuViewFlyoutViewModel()
            {
                bool isLoggedIn = ViewModelLocator.Resolve<IUserService>().IsSignIn();
                bool isAdmin = Preferences.Get(PreferenceConstants.IsAdmin, false);

                MenuItems = new ObservableCollection<MenuViewFlyoutMenuItem>(new[]
                {
                    new MenuViewFlyoutMenuItem { Id = 0, Title = "Home",TargetType=typeof(HomeView) }
                });

                if (isLoggedIn)
                {
                    MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 1, Title = "Spare Parts", TargetType = typeof(SpareListView) });
                    MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 2, Title = "Accessories", TargetType = typeof(AccessoriesListView) });
                    if (isAdmin)
                    {
                        MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 3, Title = "Vehicle Details", TargetType = typeof(VehicleListView) });
                        MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 4, Title = "Billing", TargetType = typeof(RegisterView) });
                    }
                    else
                    {
                        MenuItems.Add(new MenuViewFlyoutMenuItem { Id = MenuItems.Count, Title = "Vehicle Details", TargetType = typeof(VehicleDetailsView) });
                    }
                    MenuItems.Add(new MenuViewFlyoutMenuItem { Id = MenuItems.Count, Title = "Logout" });
                }
                else
                {
                    MenuItems.Add(new MenuViewFlyoutMenuItem { Id = MenuItems.Count, Title = "Login", TargetType = typeof(LoginView) });

                }
                MenuItems.Add(new MenuViewFlyoutMenuItem { Id = MenuItems.Count, Title = "About us", TargetType = typeof(AboutUsView) });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}