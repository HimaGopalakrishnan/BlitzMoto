using App.Features.Accessories.Models;
using App.Features.Accessories.Pages.Add;
using App.Features.Accessories.Services;
using App.Providers.Navigation.Services;
using App.Providers.Navigation.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using App.Providers.Database.Services;
using App.Providers.Api.Services;
using App.Providers.Dialog.Services;
using App.Resx;

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

        readonly IApiCallManager _apiCallManager;
        readonly ISQLiteService _sqliteService;
        readonly INavigationService _navigationService;
        readonly IAccessoriesService _accessoriesService;
        readonly IDialogService _dialogService;

        #endregion

        #region Command

        public ICommand AddIconTappedCommand { get; set; }

        #endregion

        #region Constructor

        public AccessoriesListViewModel(IApiCallManager apiCallManager, ISQLiteService sqliteService,
                                        INavigationService navigationService, IAccessoriesService accessoriesService,
                                        IDialogService dialogService)
        {
            _apiCallManager = apiCallManager;
            _sqliteService = sqliteService;
            _navigationService = navigationService;
            _accessoriesService = accessoriesService;
            _dialogService = dialogService;
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
            await _apiCallManager.ExecuteCall(() => _accessoriesService.GetAllAccessories(),
                    accessories =>
                    {
                        Accessories = new ObservableCollection<Accessory>(accessories);
                    },
                    async error =>
                    {
                        var accessories = await _sqliteService.GetAllItemsAsync<Accessory>();
                        Accessories = new ObservableCollection<Accessory>(accessories);
                        _dialogService.HideLoading();

                    }, true, AppResources.Loading);
        }

        #endregion
    }
}
