using Xamarin.Forms;

namespace App.Features.Vehicles.Pages.List
{
    public partial class VehicleListView : ContentPage
    {
        VehicleListViewModel vm;

        public VehicleListView()
        {
            InitializeComponent();
            BindingContext = vm = new VehicleListViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetVehicles();
        }
    }
}
