using App.Features.SpareParts.Models;
using App.Features.Vehicles.Models;
using App.Providers.Navigation.Base;
using App.Providers.Validation;
using App.Providers.Validation.Rules;
using App.Resx;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.Vehicles.Pages.Add
{
    public class AddVehicleViewModel : ViewModelBase
    {
        #region Properties

        List<string> _models;
        public List<string> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        ValidatableObject<string> _ownerName;
        public ValidatableObject<string> OwnerName
        {
            get => _ownerName;
            set => SetProperty(ref _ownerName, value);
        }
        ValidatableObject<string> _phone;
        public ValidatableObject<string> Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        ValidatableObject<string> _vehicleName;
        public ValidatableObject<string> VehicleName
        {
            get => _vehicleName;
            set => SetProperty(ref _vehicleName, value);
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

        #region Commands

        public ICommand AddVehicleCommand { get; set; }

        #endregion

        #region Constructor

        public AddVehicleViewModel()
        {
            Models = new List<string> { "Royal Enfield", "KTM", "Yamaha", "Bajaj", "Kawasaki", "BMW", "Benelli", "Harley Davids", "Honda", "Triumph", "Ducati", "Susuki" };
            OwnerName = new ValidatableObject<string>();
            Phone = new ValidatableObject<string>();
            VehicleName = new ValidatableObject<string>();
            Model = new ValidatableObject<string>();
            CaseNumber = new ValidatableObject<string>();
            EngineNumber = new ValidatableObject<string>();
            ServiceDate = new ValidatableObject<DateTime>(DateTime.Today);
            NextServiceDate = new ValidatableObject<DateTime>(DateTime.Today);
            Kilometer = new ValidatableObject<double>();
            AddValidations();

            AddVehicleCommand = new Command(async () => await AddVehicle());
        }

        #endregion

        #region Methods

        async Task AddVehicle()
        {
            if (Validate())
            {
                var vehicle = new Vehicle
                {
                    OwnerName = OwnerName.Value,
                    ContactNumber=Phone.Value,
                    VehicleName=VehicleName.Value,
                    Model = Model.Value,
                    CaseNumber = CaseNumber.Value,
                    EngineNumber = EngineNumber.Value,
                    ServiceDate = ServiceDate.Value,
                    NextServiceDate = NextServiceDate.Value,
                    Kilometer = Kilometer.Value,
                    Note = Note,
                };
                await ApiCallManager.ExecuteCall(() => VehicleService.SaveVehicleDetails(vehicle),
                        async response =>
                        {
                            //var id = await SqliteService.SaveItemAsync(vehicle);
                            DialogService.Toast(AppResources.Data_Added);
                            await UserService.CreateUser(OwnerName.Value, Phone.Value, EngineNumber.Value);
                            if (vehicle.SpareUsed != null && vehicle.SpareCount > 0)
                            {
                                await UpdateSpareDetails(vehicle);
                            }
                            await NavigationService.RemoveLastPageAsync();
                        },
                        error =>
                        {
                            DialogService.HideLoading();

                        }, showBusy: true, busyMessage: AppResources.Loading, ignoreCache: true);
            }
        }

        async Task UpdateSpareDetails(Vehicle vehicle)
        {
            var spare = vehicle.SpareUsed;
            spare.Quantity--;
            await ApiCallManager.ExecuteCall(() => SpareService.UpdateSpare(spare),
                    async response =>
                    {
                        await SqliteService.SaveItemAsync(spare);
                    },
                    error =>
                    {
                        DialogService.HideLoading();

                    }, true, AppResources.Loading);
        }

        bool Validate()
        {
            _ownerName.Validate();
            _model.Validate();
            _caseNumber.Validate();
            _engineNumber.Validate();
            _serviceDate.Validate();
            _nextServiceDate.Validate();
            _kilometer.Validate();
            return _ownerName.IsValid && _model.IsValid && _caseNumber.IsValid && _engineNumber.IsValid && _serviceDate.IsValid && _nextServiceDate.IsValid && _kilometer.IsValid;
        }

        void AddValidations()
        {
            _ownerName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Enter valid name" });
            _phone.Validations.Add(new IsValidContactNumberRule<string> { ValidationMessage = "Enter valid contact number" });
            _vehicleName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Enter valid vehicle name" });
            _model.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Enter valid model" });
            _caseNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Data });
            _engineNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Data });
            _serviceDate.Validations.Add(new IsValidDateRule<DateTime> { ValidationMessage = "Enter date" });
            _nextServiceDate.Validations.Add(new IsValidDateRule<DateTime> { ValidationMessage = "Enter next service date" });
            _kilometer.Validations.Add(new IsValidNumericRule<double> { ValidationMessage = "Enter next service kilometer" });
        }

        #endregion
    }
}