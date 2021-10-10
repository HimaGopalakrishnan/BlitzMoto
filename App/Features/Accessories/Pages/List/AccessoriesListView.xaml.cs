using Xamarin.Forms;

namespace App.Features.Accessories.Pages.List
{
    public partial class AccessoriesListView : ContentPage
    {
        public AccessoriesListView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var vm = BindingContext as AccessoriesListViewModel; 
            await vm.GetItems();
        }
    }
}
