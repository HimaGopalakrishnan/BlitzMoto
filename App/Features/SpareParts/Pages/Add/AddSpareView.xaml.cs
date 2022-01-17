using Xamarin.Forms;

namespace App.Features.SpareParts.Pages.Add
{
    public partial class AddSpareView : ContentPage
    {
        AddSpareViewModel vm;

        public AddSpareView()
        {
            InitializeComponent();
            BindingContext = vm = new AddSpareViewModel();
        }
    }
}
