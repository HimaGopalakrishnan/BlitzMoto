using Xamarin.Forms;

namespace App.Features.Accessories.Pages.Add
{
    public partial class AddAccessoriesView : ContentPage
    {
        AddAccessoriesViewModel vm;

        public AddAccessoriesView()
        {
            InitializeComponent();
            BindingContext = vm = new AddAccessoriesViewModel();
        }
    }
}
