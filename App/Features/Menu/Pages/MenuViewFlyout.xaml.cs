using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Constants;
using App.Features.About.Pages;
using App.Features.Accessories.Pages.List;
using App.Features.Home;
using App.Features.SpareParts.Pages.List;
using App.Features.User.Pages.Login;
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
                bool isLoggedIn = Preferences.Get(PreferenceConstants.IsLoggedIn, false);
                bool isOwner = Preferences.Get(PreferenceConstants.IsOwner, false);

                MenuItems = new ObservableCollection<MenuViewFlyoutMenuItem>(new[]
                {
                    new MenuViewFlyoutMenuItem { Id = 0, Title = "Home",TargetType=typeof(HomeView) }
                });

                if (isLoggedIn)
                {
                    MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 1, Title = "Spare Parts", TargetType = typeof(SpareListView) });
                    MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 2, Title = "Accessories", TargetType = typeof(AccessoriesListView) });
                    if (isOwner)
                    {
                        MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 3, Title = "Vehicle Details", TargetType = typeof(LoginView) });
                        MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 4, Title = "Billing", TargetType = typeof(LoginView) });
                    }
                }
                else
                {
                    MenuItems.Add(new MenuViewFlyoutMenuItem { Id = 1, Title = "Login", TargetType = typeof(LoginView) });

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