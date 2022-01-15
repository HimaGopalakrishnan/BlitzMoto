using App.Features.SpareParts.Models;
using App.Features.SpareParts.Services;
using App.Features.Vehicles.Models;
using App.Features.Vehicles.Services;
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

namespace App.Features.Vehicles.Pages.Add
{
    public class AddVehicleViewModel : ViewModelBase
    {
        #region Properties

        ValidatableObject<string> _name;
        public ValidatableObject<string> Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        ValidatableObject<string> _model;
        public ValidatableObject<string> Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }
        ValidatableObject<string> _caseNumber;
        public ValidatableObject<string> CaseNumber
        {
            get => _caseNumber;
            set => SetProperty(ref _caseNumber, value);
        }
        ValidatableObject<string> _engineNumber;
        public ValidatableObject<string> EngineNumber
        {
            get => _engineNumber;
            set => SetProperty(ref _engineNumber, value);
        }
        ValidatableObject<DateTime> _serviceDate;
        public ValidatableObject<DateTime> ServiceDate
        {
            get => _serviceDate;
            set => SetProperty(ref _serviceDate, value);
        }
        ValidatableObject<DateTime> _nextServiceDate;
        public ValidatableObject<DateTime> NextServiceDate
        {
            get => _nextServiceDate;
            set => SetProperty(ref _nextServiceDate, value);
        }
        SparePart _spare;
        public SparePart Spare
        {
            get => _spare;
            set => SetProperty(ref _spare, value);
        }
        int _spareCount;
        public int SpareCount
        {
            get => _spareCount;
            set => SetProperty(ref _spareCount, value);
        }
        ValidatableObject<double> _kilometer;
        public ValidatableObject<double> Kilometer
        {
            get => _kilometer;
            set => SetProperty(ref _kilometer, value);
        }
        string _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        #endregion

        #region Services

        readonly IApiCallManager _apiCallManager;
        readonly ISQLiteService _sqliteService;
        readonly INavigationService _navigationService;
        readonly IVehicleService _vehicleService;
        readonly ISpareService _spareService;
        readonly IDialogService _dialogService;

        #endregion

        #region Command

        public ICommand SaveButtonTapped { get; set; }

        #endregion

        #region Constructor

        public AddVehicleViewModel(IApiCallManager apiCallManager, ISQLiteService sqliteService,
                                   INavigationService navigationService, IVehicleService vehicleService,
                                   ISpareService spareService, IDialogService dialogService)
        {
            _apiCallManager = apiCallManager;
            _sqliteService = sqliteService;
            _navigationService = navigationService;
            _vehicleService = vehicleService;
            _spareService = spareService;
            _dialogService = dialogService;

            Name = new ValidatableObject<string>();
            Model = new ValidatableObject<string>();
            CaseNumber = new ValidatableObject<string>();
            EngineNumber = new ValidatableObject<string>();
            ServiceDate = new ValidatableObject<DateTime>();
            NextServiceDate = new ValidatableObject<DateTime>();
            Kilometer = new ValidatableObject<double>();
            AddValidations();

            SaveButtonTapped = new Command(async () => await SaveVehicleDetails());
        }

        #endregion

        #region Methods

        async Task SaveVehicleDetails()
        {
            if (Validate())
            {
                var vehicle = new Vehicle
                {
                    Name = Name.Value,
                    Model = Model.Value,
                    CaseNumber = CaseNumber.Value,
                    EngineNumber = EngineNumber.Value,
                    ServiceDate = ServiceDate.Value,
                    NextServiceDate = NextServiceDate.Value,
                    Kilometer = Kilometer.Value,
                    Note = Note,
                };
                await _apiCallManager.ExecuteCall(() => _vehicleService.SaveVehicleDetails(vehicle),
                        async response =>
                        {
                            var id = await _sqliteService.SaveItemAsync(vehicle);
                            _dialogService.Toast(AppResources.Data_Added);
                            if (vehicle.SpareUsed != null && vehicle.SpareCount > 0)
                            {
                                await UpdateSpareDetails(vehicle);
                            }
                            await _navigationService.RemoveLastPageAsync();
                        },
                        error =>
                        {
                            _dialogService.HideLoading();

                        }, true, AppResources.Loading);
            }
        }

        async Task UpdateSpareDetails(Vehicle vehicle)
        {
            var spare = vehicle.SpareUsed;
            spare.Quantity--;
            await _apiCallManager.ExecuteCall(() => _spareService.UpdateSpare(spare),
                    async response =>
                    {
                        await _sqliteService.SaveItemAsync(spare);
                    },
                    error =>
                    {
                        _dialogService.HideLoading();

                    }, true, AppResources.Loading);
        }

        bool Validate()
        {
            _name.Validate();
            _model.Validate();
            _caseNumber.Validate();
            _engineNumber.Validate();
            _serviceDate.Validate();
            _nextServiceDate.Validate();
            _kilometer.Validate();
            return _name.IsValid && _model.IsValid && _caseNumber.IsValid && _engineNumber.IsValid && _serviceDate.IsValid && _nextServiceDate.IsValid && _kilometer.IsValid;
        }

        void AddValidations()
        {
            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Enter valid name" });
            _model.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Enter valid model" });
            _caseNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Data });
            _engineNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Data });
            _serviceDate.Validations.Add(new IsValidDateRule<DateTime> { ValidationMessage = "Enter date" });
            _nextServiceDate.Validations.Add(new IsValidDateRule<DateTime> { ValidationMessage = "Enter next service date" });
            _kilometer.Validations.Add(new IsValidNumericRule<double> { ValidationMessage = "Enter next service kilometer" });
        }

        #endregion

        #region Override Methods

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                Model = navigationData as ValidatableObject<string>;
            }
            return base.InitializeAsync(navigationData);
        }

        #endregion
    }
}