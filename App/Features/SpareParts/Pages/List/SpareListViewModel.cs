using App.Features.Accessories.Pages.Add;
using App.Features.SpareParts.Models;
using App.Features.SpareParts.Services;
using App.Providers.Api.Services;
using App.Providers.Database.Services;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using App.Providers.Navigation.Services;
using App.Resx;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.SpareParts.Pages.List
{
    public class SpareListViewModel : ViewModelBase
    {
        #region Fields

        List<SparePart> allSpares = new List<SparePart>();

        #endregion

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

        public ICommand SearchCommand { get; set; }
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

            SearchCommand = new Command<string>(Search);
            AddIconTappedCommand = new Command(async () => await AddIconTapped());
        }

        #endregion

        #region Methods

        void Search(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResult = allSpares.Where(x => x.Name.Equals(searchText)).ToList();
                Spares = new ObservableCollection<SparePart>(searchResult);
            }
            else
            {
                Spares = new ObservableCollection<SparePart>(allSpares);
            }
        }

        async Task AddIconTapped()
        {
            await _navigationService.NavigateToAsync<AddAccessoriesViewModel>();
        }

        public async Task GetItems()
        {
            var spares = await _sqliteService.GetAllItemsAsync<SparePart>();
            Spares = new ObservableCollection<SparePart>(spares);
            await _apiCallManager.ExecuteCall(() => _spareService.GetAllSpares(),
                    spares =>
                    {
                        allSpares = spares;
                        Spares = new ObservableCollection<SparePart>(spares);
                    },
                    async error =>
                    {
                        var _spares = await _sqliteService.GetAllItemsAsync<SparePart>();
                        Spares = new ObservableCollection<SparePart>(_spares);
                        _dialogService.HideLoading();

                    }, true, AppResources.Loading);
        }

        #endregion

        #region Override Methods

        public override async Task InitializeAsync(object navigationData)
        {
            await GetItems();
        }

        #endregion
    }
}
