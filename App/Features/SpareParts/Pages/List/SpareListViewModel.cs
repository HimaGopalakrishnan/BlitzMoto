using App.Features.Accessories.Pages.Add;
using App.Providers.Navigation.Services;
using App.Providers.Navigation.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using App.Features.SpareParts.Models;
using App.Providers.Database.Services;
using App.Features.SpareParts.Services;
using App.Providers.Api.Services;
using App.Resx;
using App.Providers.Dialog.Services;

namespace App.Features.SpareParts.Pages.List
{
    public class SpareListViewModel : ViewModelBase
    {
        #region Properties

        ObservableCollection<SparePart> _spares;
        public ObservableCollection<SparePart> Spares
        {
            get => _spares;
            set => SetProperty(ref _spares, value);
        }

        #endregion

        #region Services

        readonly IApiCallManager _apiCallManager;
        readonly ISQLiteService _sqliteService;
        readonly ISpareService _spareService;
        readonly INavigationService _navigationService;
        readonly IDialogService _dialogService;

        #endregion

        #region Command

        public ICommand AddIconTappedCommand { get; set; }

        #endregion

        #region Constructor

        public SpareListViewModel(IApiCallManager apiCallManager, ISQLiteService sqliteService,
                                  ISpareService spareService, INavigationService navigationService,
                                  IDialogService dialogService)
        {
            _apiCallManager = apiCallManager;
            _sqliteService = sqliteService;
            _spareService = spareService;
            _navigationService = navigationService;
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
            var spares = await _sqliteService.GetAllItemsAsync<SparePart>();
            Spares = new ObservableCollection<SparePart>(spares);
            await _apiCallManager.ExecuteCall(() => _spareService.GetAllSpares(),
                    accessories =>
                    {
                        Spares = new ObservableCollection<SparePart>(accessories);
                    },
                    async error =>
                    {
                        var _spares = await _sqliteService.GetAllItemsAsync<SparePart>();
                        Spares = new ObservableCollection<SparePart>(_spares);
                        _dialogService.HideLoading();

                    }, true, AppResources.Loading);
        }

        #endregion
    }
}
