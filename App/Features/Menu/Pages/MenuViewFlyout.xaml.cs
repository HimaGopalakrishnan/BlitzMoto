using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Features.About.Pages;
using App.Features.Accessories.Pages.List;
using App.Features.Home;
using App.Features.SpareParts.Pages.List;
using App.Features.User.Pages.Login;
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
                MenuItems = new ObservableCollection<MenuViewFlyoutMenuItem>(new[]
                {
                    new MenuViewFlyoutMenuItem { Id = 0, Title = "Home",TargetType=typeof(HomeView) },
                    new MenuViewFlyoutMenuItem { Id = 1, Title = "Spare Parts",TargetType=typeof(SpareListView) },
                    new MenuViewFlyoutMenuItem { Id = 2, Title = "Accessories",TargetType=typeof(AccessoriesListView) },
                    new MenuViewFlyoutMenuItem { Id = 3, Title = "Login",TargetType=typeof(LoginView) },
                    new MenuViewFlyoutMenuItem { Id = 4, Title = "About us",TargetType=typeof(AboutUsView) },
                });
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