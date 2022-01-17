using System;
using App.Features.User.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Features.Menu.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : FlyoutPage
    {
        public MenuView()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuViewFlyoutMenuItem;
            if (item == null)
            {
                return;
            }
            try
            {
                if (item.Title == "Logout")
                {
                    DependencyService.Get<IUserService>().SignOut();
                }
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
            }
            catch (Exception ex)
            {

            }
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }
    }
}