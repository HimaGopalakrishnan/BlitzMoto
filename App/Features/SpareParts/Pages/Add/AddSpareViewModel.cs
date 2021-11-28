using App.Features.SpareParts.Models;
using App.Features.SpareParts.Services;
using App.Providers.Api.Services;
using App.Providers.Database.Services;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using App.Providers.Navigation.Services;
using App.Providers.Validation;
using App.Providers.Validation.Rules;
using App.Resx;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.SpareParts.Pages.Add
{
    public class AddSpareViewModel : ViewModelBase
    {
        #region Services

        readonly IApiCallManager _apiCallManager;
        readonly ISpareService _spareService;
        readonly ISQLiteService _sqliteService;
        readonly INavigationService _navigationService;
        readonly IDialogService _dialogService;

        #endregion

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

        ValidatableObject<string> _spare;
        public ValidatableObject<string> Spare
        {
            get => _spare;
            set => SetProperty(ref _spare, value);
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

        #endregion

        #region Constructor

        public AddSpareViewModel(IApiCallManager apiCallManager,ISpareService spareService,
                                 ISQLiteService sqliteService, INavigationService navigationService,
                                 IDialogService dialogService)
        {
            _apiCallManager = apiCallManager;
            _spareService = spareService;
            _sqliteService = sqliteService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            AddCommand = new Command(async () => await Add());

            PartNumber = new ValidatableObject<string>();
            Spare = new ValidatableObject<string>();
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
                var spare = new SparePart { Number = PartNumber.Value, Name = Spare.Value, Quantity = Convert.ToInt32(Quantity.Value), Price = Convert.ToDouble(Price.Value) };
                await _apiCallManager.ExecuteCall(() => _spareService.AddSpare(spare),
                    async response =>
                    {
                        var id = await _sqliteService.SaveItemAsync(spare);
                        _dialogService.Toast(AppResources.Data_Added);
                        await _navigationService.RemoveLastPageAsync();
                    },
                    error =>
                    {
                        _dialogService.HideLoading();

                    }, true, AppResources.Loading);
            }
        }

        public bool Validate()
        {
            _partNumber.Validate();
            _spare.Validate();
            _quantity.Validate();
            _price.Validate();
            return _partNumber.IsValid && _spare.IsValid && _quantity.IsValid && _price.IsValid;
        }

        void AddValidations()
        {
            _partNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Data });
            _spare.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Name });
            _quantity.Validations.Add(new IsValidNumericRule<string> { ValidationMessage = AppResources.Enter_Valid_Quantity });
            _price.Validations.Add(new IsValidNumericRule<string> { ValidationMessage = AppResources.Enter_Valid_Amount });
        }

        #endregion
    }
}
