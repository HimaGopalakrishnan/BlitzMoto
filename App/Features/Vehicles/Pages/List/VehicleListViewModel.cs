using App.Features.Vehicles.Models;
using App.Features.Vehicles.Pages.Add;
using App.Features.Vehicles.Pages.Detail;
using App.Providers.Navigation.Base;
using App.Resx;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.Vehicles.Pages.List
{
    public class VehicleListViewModel : ViewModelBase
    {
        #region Properties

        ObservableCollection<Vehicle> _vehicles;
        List<Vehicle> allVehicles;

        public ObservableCollection<Vehicle> Vehicles
        {
            get => _vehicles;
            set => SetProperty(ref _vehicles, value);
        }

        #endregion

        #region Command

        public ICommand SearchCommand { get; set; }
        public ICommand AddIconTappedCommand { get; set; }
        public ICommand VehicleSelectedCommand { get; set; }

        #endregion

        #region Constructor

        public VehicleListViewModel()
        {
            SearchCommand = new Command<string>(Search);
            AddIconTappedCommand = new Command(async () => await AddIconTapped());
            VehicleSelectedCommand = new Command<Vehicle>(async (vehicle) => await VehicleSelected(vehicle));
        }

        #endregion

        #region Methods

        void Search(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResult = allVehicles.Where(x => x.OwnerName.ToLower().Contains(searchText.ToLower())).ToList();
                Vehicles = new ObservableCollection<Vehicle>(searchResult);
            }
            else
            {
                Vehicles = new ObservableCollection<Vehicle>(allVehicles);
            }
        }

        async Task AddIconTapped()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddVehicleView());
        }

        async Task VehicleSelected(Vehicle vehicle)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new VehicleDetailsView(vehicle.ContactNumber));
        }

        public async Task GetVehicles()
        {
            await ApiCallManager.ExecuteCall(() => VehicleService.GetAllVehicleDetails(),
                vehicles =>
                {
                    allVehicles = vehicles;
                    Vehicles = new ObservableCollection<Vehicle>(vehicles);
                },
                async error =>
                {
                    var _vehicles = await SqliteService.GetAllItemsAsync<Vehicle>();
                    Vehicles = new ObservableCollection<Vehicle>(_vehicles);
                    DialogService.HideLoading();

                }, showBusy: true, busyMessage: AppResources.Loading, ignoreCache: true);
        }

        #endregion
    }
}