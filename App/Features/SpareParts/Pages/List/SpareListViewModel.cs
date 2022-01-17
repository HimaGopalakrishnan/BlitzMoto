using App.Features.SpareParts.Models;
using App.Features.SpareParts.Pages.Add;
using App.Providers.Navigation.Base;
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

        #region Command

        public ICommand SearchCommand { get; set; }
        public ICommand AddIconTappedCommand { get; set; }

        #endregion

        #region Constructor

        public SpareListViewModel()
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
                var searchResult = allSpares.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList();
                Spares = new ObservableCollection<SparePart>(searchResult);
            }
            else
            {
                Spares = new ObservableCollection<SparePart>(allSpares);
            }
        }

        async Task AddIconTapped()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddSpareView());
        }

        public async Task GetItems()
        {
            await ApiCallManager.ExecuteCall(() => SpareService.GetAllSpares(),
                    spares =>
                    {
                        allSpares = spares;
                        Spares = new ObservableCollection<SparePart>(spares);
                    },
                    async error =>
                    {
                        var _spares = await SqliteService.GetAllItemsAsync<SparePart>();
                        Spares = new ObservableCollection<SparePart>(_spares);
                        DialogService.HideLoading();

                    }, showBusy: true, busyMessage: AppResources.Loading, ignoreCache: true);
        }

        #endregion
    }
}
