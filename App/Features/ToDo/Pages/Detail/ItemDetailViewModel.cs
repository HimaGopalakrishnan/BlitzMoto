using System.Threading.Tasks;
using App.Features.List.Services;
using App.Providers.Api.Services;
using App.Providers.Navigation.Base;
using App.Resx;

namespace App.Features.ToDo.Pages.Detail
{
    public class ItemDetailViewModel : ViewModelBase
    {
        #region Properties

        string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        string _location;
        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        #endregion

        #region Services

        readonly IApiCallManager _apiCallManager;
        readonly IItemsService _itemsService;

        #endregion

        #region Constructor

        public ItemDetailViewModel(IApiCallManager apiCallManager,
                                   IItemsService itemsService)
        {
            _apiCallManager = apiCallManager;
            _itemsService = itemsService;
        }

        #endregion

        #region Methods

        public async Task GetItemDetail(int id)
        {
            await _apiCallManager.ExecuteCall(() => _itemsService.GetItemDetails(id),
                itemDetail =>
                {
                    Name = itemDetail.Name;
                    Address = $"{ itemDetail.City},{itemDetail.Street},{itemDetail.State},{itemDetail.Country}";
                    Location = $"{ AppResources.Latitude}:{itemDetail.Latitude}&{AppResources.Longitude}:{itemDetail.Longitude}";
                },
                error =>
                {

                }, cachekey: string.Format(Constants.Api.GetItemsDetailKey, id));
        }

        #endregion

        #region Override Methods

        public override async Task InitializeAsync(object navigationData)
        {
            var id = (int)navigationData;
            await GetItemDetail(id);
        }

        #endregion

    }
}
