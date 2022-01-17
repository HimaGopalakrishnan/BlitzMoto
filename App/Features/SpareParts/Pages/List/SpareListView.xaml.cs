using Xamarin.Forms;

namespace App.Features.SpareParts.Pages.List
{
    public partial class SpareListView : ContentPage
    {
        SpareListViewModel vm;

        public SpareListView()
        {
            InitializeComponent();
            BindingContext = vm = new SpareListViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetItems();
        }
    }
}
