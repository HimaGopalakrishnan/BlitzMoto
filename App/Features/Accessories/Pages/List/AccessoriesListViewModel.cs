using App.Features.Accessories.Models;
using App.Features.Accessories.Pages.Add;
using App.Providers.Navigation.Base;
using App.Resx;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.Accessories.Pages.List
{
    public class AccessoriesListViewModel : ViewModelBase
    {
        #region Fields

        List<Accessory> allAccessories = new List<Accessory>();

        #endregion

        #region Properties

        ObservableCollection<Accessory> _accessories;
        public ObservableCollection<Accessory> Accessories
        {
            get => _accessories;
            set => SetProperty(ref _accessories, value);
        }

        #endregion

        #region Command

        public ICommand SearchCommand { get; set; }
        public ICommand AddIconTappedCommand { get; set; }

        #endregion

        #region Constructor

        public AccessoriesListViewModel()
        {
            SearchCommand = new Command<string>(Search);
            AddIconTappedCommand = new Command(async () => await AddIconTapped());
        }

        #endregion

        #region Methods

        void Search(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResult = allAccessories.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList();
                Accessories = new ObservableCollection<Accessory>(searchResult);
            }
            else
            {
                Accessories = new ObservableCollection<Accessory>(allAccessories);
            }
        }

        async Task AddIconTapped()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddAccessoriesView());
        }

        public async Task GetItems()
        {
            await ApiCallManager.ExecuteCall(() => AccessoriesService.GetAllAccessories(),
                    accessories =>
                    {
                        allAccessories = accessories;
                        Accessories = new ObservableCollection<Accessory>(accessories);
                    },
                    async error =>
                    {
                        var accessories = await SqliteService.GetAllItemsAsync<Accessory>();
                        Accessories = new ObservableCollection<Accessory>(accessories);
                        DialogService.HideLoading();

                    }, showBusy: true, busyMessage: AppResources.Loading, ignoreCache: true);
        }

        #endregion
    }
}