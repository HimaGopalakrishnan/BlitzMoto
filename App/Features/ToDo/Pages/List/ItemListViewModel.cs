using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Features.List.Services;
using App.Features.ToDo.Models;
using App.Features.ToDo.Pages.Detail;
using App.Provider.Navigation.Services;
using App.Providers.Api.Services;
using App.Providers.Navigation.Base;
using Xamarin.Forms;

namespace App.Features.ToDo.Pages.List
{
    public class ItemListViewModel: ViewModelBase
    {
        #region Properties

        ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        #endregion

        #region Services

        readonly IApiCallManager _apiCallManager;
        readonly INavigationService _navigationService;
        readonly IItemsService _itemsService;

        #endregion

        #region Command

        public ICommand ItemTappedCommand { get; set; }

        #endregion

        #region Constructor

        public ItemListViewModel(IApiCallManager apiCallManager,
                                 INavigationService navigationService,
                                 IItemsService itemsService)
        {
            _apiCallManager = apiCallManager;
            _navigationService = navigationService;
            _itemsService = itemsService;
            ItemTappedCommand = new Command<int>(async (id) => await NavigateToDetailPage(id));
        }

        #endregion

        #region Methods

        async Task NavigateToDetailPage(int id)
        {
            await _navigationService.NavigateToAsync<ItemDetailViewModel>(id);
        }

        public async Task GetItems()
        {
            await _apiCallManager.ExecuteCall(() => _itemsService.GetItems(),
                items =>
                {
                    Items = new ObservableCollection<Item>(items);
                },
                error =>
                {

                }, cachekey: Constants.Api.GetItemsKey);
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
