using App.Constants;
using App.Features.SpareParts.Models;
using App.Features.Vehicles.Models;
using App.Providers.Navigation.Base;
using App.Resx;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

        #region Constructor

        public VehicleDetailsViewModel()
        {
            IsAdmin = Preferences.Get(PreferenceConstants.IsAdmin, false);
        }

        #endregion

        #region Methods

        public async Task GetVehicleDetails(string caseNumber)
        {
            Vehicle vehicle = null;

            await ApiCallManager.ExecuteCall(() => VehicleService.GetVehicleDetails(caseNumber),
                    response =>
                    {
                        IsVisible = response != null;
                        vehicle = response;
                    },
                    async error =>
                    {
                        IsVisible = false;
                        vehicle = await SqliteService.GetItemAsync<Vehicle>(0);
                        DialogService.HideLoading();

                    }, true, AppResources.Loading);

            if (vehicle != null)
            {
                Name = vehicle.OwnerName;
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

        #endregion
    }
}