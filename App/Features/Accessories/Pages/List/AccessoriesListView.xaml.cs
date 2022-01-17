using Xamarin.Forms;

namespace App.Features.Accessories.Pages.List
{
    public partial class AccessoriesListView : ContentPage
    {
        AccessoriesListViewModel vm;
        public AccessoriesListView()
        {
            InitializeComponent();
            BindingContext = vm = new AccessoriesListViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetItems();
        }
    }
}
