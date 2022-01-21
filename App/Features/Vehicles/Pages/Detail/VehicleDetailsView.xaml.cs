using App.Constants;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.Features.Vehicles.Pages.Detail
{
    public partial class VehicleDetailsView : ContentPage
    {
        VehicleDetailsViewModel vm;
        string contactNumber;

        public VehicleDetailsView()
        {
            InitializeComponent();
            BindingContext = vm = new VehicleDetailsViewModel();
        }

        public VehicleDetailsView(string contactNumber)
        {
            InitializeComponent();
            BindingContext = vm = new VehicleDetailsViewModel();
            this.contactNumber = contactNumber;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (string.IsNullOrEmpty(contactNumber))
            {
                contactNumber = Preferences.Get(PreferenceConstants.Mobile, "");
            }
            await vm.GetVehicleDetails(contactNumber);
        }
    }
}
