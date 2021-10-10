using System.Threading.Tasks;
using System.Windows.Input;
using App.Provider.Navigation.Services;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using Xamarin.Forms;

namespace App.Features.Accessories.Pages.Add
{
    public class AddAccessoriesViewModel : ViewModelBase
    {
        #region Services

        readonly INavigationService _navigationService;
        readonly IDialogService _dialogService;

        #endregion

        #region Commands

        public ICommand AddCommand { get; set; }

        #endregion

        #region Properties

        string _partNumber;
        public string PartNumber
        {
            get => _partNumber;
            set => SetProperty(ref _partNumber, value);
        }

        string _accessory;
        public string Accessory
        {
            get => _accessory;
            set => SetProperty(ref _accessory, value);
        }

        double _quantity;
        public double Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        double _price;
        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        #endregion

        #region Constructor

        public AddAccessoriesViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            AddCommand = new Command(async () => await Add());
        }

        #endregion

        #region Methods

        async Task Add()
        {
            await Task.Delay(1);
            bool isValid = Validate();
            if (isValid)
            {
                //add api call here
            }
            else
            {
                _dialogService.Alert("Login Failed");
            }
        }

        public bool Validate()
        {
            return true;
        }

        #endregion
    }
}
