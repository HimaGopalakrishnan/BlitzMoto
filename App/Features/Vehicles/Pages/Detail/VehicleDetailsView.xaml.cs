using Xamarin.Forms;

namespace App.Features.Vehicles.Pages.Detail
{
    public partial class VehicleDetailsView : ContentPage
    {
        VehicleDetailsViewModel vm;
        string caseNumber;

        public VehicleDetailsView(string caseNumber)
        {
            InitializeComponent();
            BindingContext = vm = new VehicleDetailsViewModel();
            this.caseNumber = caseNumber;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetVehicleDetails(caseNumber);
        }
    }
}
