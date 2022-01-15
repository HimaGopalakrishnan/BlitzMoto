using App.Features.Vehicles.Models;
using App.Features.Vehicles.Pages.Detail;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using App.Providers.Navigation.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.Vehicles.Pages.List
{
    public class VehicleListViewModel : ViewModelBase
    {
        #region Properties

        ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get => _vehicles;
            set => SetProperty(ref _vehicles, value);
        }

        #endregion

        #region Services

        readonly INavigationService _navigationService;
        readonly IDialogService _dialogService;

        #endregion

        #region Command

        public ICommand VehicleSelectedCommand { get; set; }

        #endregion

        #region Constructor

        public VehicleListViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            VehicleSelectedCommand = new Command<string>(async (model) => await VehicleSelected(model));
            GetItems();
        }

        #endregion

        #region Methods

        async Task VehicleSelected(string model)
        {
            await _navigationService.NavigateToAsync<VehicleDetailsViewModel>(model);
        }

        public void GetItems()
        {
            Vehicles = new ObservableCollection<Vehicle>
            {
                new Vehicle { Model = "Royal Enfield" },
                new Vehicle { Model = "KTM" },
                new Vehicle { Model = "Yamaha" },
                new Vehicle { Model = "Bajaj" },
                new Vehicle { Model = "Kawasaki" },
                new Vehicle { Model = "BMW" },
                new Vehicle { Model = "Benelli" },
                new Vehicle { Model = "Harley Davidson" },
                new Vehicle { Model = "Honda" },
                new Vehicle { Model = "Triumph" },
                new Vehicle { Model = "Ducati" },
                new Vehicle { Model = "Susuki" },
            };
        }

        #endregion
    }
}