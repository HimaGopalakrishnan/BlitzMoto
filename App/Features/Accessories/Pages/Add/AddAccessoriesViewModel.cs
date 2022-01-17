using App.Features.Accessories.Models;
using App.Providers.Navigation.Base;
using App.Providers.Validation;
using App.Providers.Validation.Rules;
using App.Resx;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.Accessories.Pages.Add
{
    public class AddAccessoriesViewModel : ViewModelBase
    {
        #region Commands

        public ICommand AddCommand { get; set; }

        #endregion

        #region Properties

        ValidatableObject<string> _partNumber;
        public ValidatableObject<string> PartNumber
        {
            get => _partNumber;
            set => SetProperty(ref _partNumber, value);
        }

        ValidatableObject<string> _accessory;
        public ValidatableObject<string> Accessory
        {
            get => _accessory;
            set => SetProperty(ref _accessory, value);
        }

        ValidatableObject<string> _quantity;
        public ValidatableObject<string> Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        ValidatableObject<string> _price;
        public ValidatableObject<string> Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        #endregion

        #region Constructor

        public AddAccessoriesViewModel()
        {
            AddCommand = new Command(async () => await Add());

            PartNumber = new ValidatableObject<string>();
            Accessory = new ValidatableObject<string>();
            Quantity = new ValidatableObject<string>();
            Price = new ValidatableObject<string>();

            AddValidations();
        }

        #endregion

        #region Methods

        async Task Add()
        {
            await Task.Delay(1);
            bool isValid = Validate();
            if (isValid)
            {
                var accessory = new Accessory { Number = PartNumber.Value, Name = Accessory.Value, Quantity = Convert.ToInt32(Quantity.Value), Price = Convert.ToDouble(Price.Value) };
                await ApiCallManager.ExecuteCall(() => AccessoriesService.AddAccessory(accessory),
                    async response =>
                    {
                        var id = await SqliteService.SaveItemAsync(accessory);
                        DialogService.Toast(AppResources.Data_Added);
                        await NavigationService.RemoveLastPageAsync();
                    },
                    error =>
                    {
                        DialogService.HideLoading();

                    }, showBusy: true, busyMessage: AppResources.Loading, ignoreCache: true);
            }
        }

        public bool Validate()
        {
            _partNumber.Validate();
            _accessory.Validate();
            _quantity.Validate();
            _price.Validate();
            return _partNumber.IsValid && _accessory.IsValid && _quantity.IsValid && _price.IsValid;
        }

        void AddValidations()
        {
            _partNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Data });
            _accessory.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Name });
            _quantity.Validations.Add(new IsValidNumericRule<string> { ValidationMessage = AppResources.Enter_Valid_Quantity });
            _price.Validations.Add(new IsValidNumericRule<string> { ValidationMessage = AppResources.Enter_Valid_Amount });
        }

        #endregion
    }
}
