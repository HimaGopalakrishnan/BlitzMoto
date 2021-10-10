using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Features.Accessories.Models;
using App.Features.Accessories.Pages.Add;
using App.Features.Accessories.Services;
using App.Provider.Navigation.Services;
using App.Providers.Navigation.Base;
using Xamarin.Forms;

namespace App.Features.Accessories.Pages.List
{
    public class AccessoriesListViewModel : ViewModelBase
    {
        #region Properties

        ObservableCollection<Accessory> _accessories;
        public ObservableCollection<Accessory> Accessories
        {
            get => _accessories;
            set => SetProperty(ref _accessories, value);
        }

        #endregion

        #region Services

        readonly INavigationService _navigationService;
        readonly IAccessoriesService _accessoriesService;

        #endregion

        #region Command

        public ICommand AddIconTappedCommand { get; set; }

        #endregion

        #region Constructor

        public AccessoriesListViewModel(INavigationService navigationService,
                                        IAccessoriesService accessoriesService)
        {
            _navigationService = navigationService;
            _accessoriesService = accessoriesService;
            AddIconTappedCommand = new Command(async () => await AddIconTapped());
        }

        #endregion

        #region Methods

        async Task AddIconTapped()
        {
            await _navigationService.NavigateToAsync<AddAccessoriesViewModel>();
        }

        public async Task GetItems()
        {
            await Task.Delay(1);
            Accessories = new ObservableCollection<Accessory>
            {
                new Accessory{Name="A"},
                new Accessory{Name="B"},
                new Accessory{Name="C"},
                new Accessory{Name="D"},
                new Accessory{Name="E"},
                new Accessory{Name="F"},
                new Accessory{Name="G"},
                new Accessory{Name="H"}
            };
        }

        #endregion
    }
}
