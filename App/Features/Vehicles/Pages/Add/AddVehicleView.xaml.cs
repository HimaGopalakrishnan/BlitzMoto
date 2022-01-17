using Xamarin.Forms;

namespace App.Features.Vehicles.Pages.Add
{
    public partial class AddVehicleView : ContentPage
    {
        AddVehicleViewModel vm;

        public AddVehicleView()
        {
            InitializeComponent();
            BindingContext = vm = new AddVehicleViewModel();
        }
    }
}
