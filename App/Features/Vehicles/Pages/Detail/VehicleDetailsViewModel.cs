using App.Constants;
using App.Features.SpareParts.Models;
using App.Features.Vehicles.Models;
using App.Features.Vehicles.Pages.Add;
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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.Features.Vehicles.Pages.Detail
{
    public class VehicleDetailsViewModel : ViewModelBase
    {
        #region Properties

        bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        bool _isDataFetched;
        public bool IsDataFetched
        {
            get => _isDataFetched;
            set => SetProperty(ref _isDataFetched, value);
        }

        public bool IsAdmin { get; set; }

        ValidatableObject<string> _vehicleNumber;
        public ValidatableObject<string> VehicleNumber
        {
            get => _vehicleNumber;
            set => SetProperty(ref _vehicleNumber, value);
        }

        string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        string _model;
        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        string _caseNumber;
        public string CaseNumber
        {
            get => _caseNumber;
            set => SetProperty(ref _caseNumber, value);
        }

        string _engineNumber;
        public string EngineNumber
        {
            get => _engineNumber;
            set => SetProperty(ref _engineNumber, value);
        }

        DateTime _serviceDate;
        public DateTime ServiceDate
        {
            get => _serviceDate;
            set => SetProperty(ref _serviceDate, value);
        }

        DateTime _nextServiceDate;
        public DateTime NextServiceDate
        {
            get => _nextServiceDate;
            set => SetProperty(ref _nextServiceDate, value);
        }

        double _kilometer;
        public double Kilometer
        {
            get => _kilometer;
            set => SetProperty(ref _kilometer, value);
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
        readonly IDialogService _dialogService;

        #endregion

        #region Command

        public ICommand FetchDataCommand { get; set; }
        public ICommand AddVehicleCommand { get; set; }

        #endregion

        #region Constructor

        public VehicleDetailsViewModel(IApiCallManager apiCallManager, ISQLiteService sqliteService,
                                        INavigationService navigationService, IVehicleService vehicleService,
                                        IDialogService dialogService)
        {
            _apiCallManager = apiCallManager;
            _sqliteService = sqliteService;
            _navigationService = navigationService;
            _vehicleService = vehicleService;
            _dialogService = dialogService;

            IsAdmin = Preferences.Get(PreferenceConstants.IsAdmin, false);
            VehicleNumber = new ValidatableObject<string>();
            AddValidations();

            FetchDataCommand = new Command(FetchData);
            AddVehicleCommand = new Command(async () => await NavigateToAddVehiclePage());
        }

        #endregion

        #region Methods

        void FetchData()
        {
            IsDataFetched = true;
        }

        async Task NavigateToAddVehiclePage()
        {
            await _navigationService.NavigateToAsync<AddVehicleViewModel>();
        }

        async Task GetVehicleDetails()
        {
            if (Validate())
            {
                Vehicle vehicle = null;

                await _apiCallManager.ExecuteCall(() => _vehicleService.GetVehicleDetails(0),
                        response =>
                        {
                            IsVisible = response != null;
                            vehicle = response;
                        },
                        async error =>
                        {
                            IsVisible = false;
                            vehicle = await _sqliteService.GetItemAsync<Vehicle>(0);
                            _dialogService.HideLoading();

                        }, true, AppResources.Loading);

                if (vehicle != null)
                {
                    Name = vehicle.Name;
                    Model = vehicle.Model;
                    CaseNumber = vehicle.CaseNumber;
                    EngineNumber = vehicle.EngineNumber;
                    ServiceDate = vehicle.ServiceDate;
                    NextServiceDate = vehicle.NextServiceDate;
                    Spare = vehicle.SpareUsed;
                    SpareCount = vehicle.SpareCount;
                    Kilometer = vehicle.Kilometer;
                    Note = vehicle.Note;
                }
            }
        }

        bool Validate()
        {
            _vehicleNumber.Validate();
            return _vehicleNumber.IsValid;
        }

        void AddValidations()
        {
            _vehicleNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.Enter_Valid_Data });
        }

        #endregion

        #region Override Methods

        public override async Task InitializeAsync(object navigationData)
        {
            await GetVehicleDetails();
        }

        #endregion
    }
}