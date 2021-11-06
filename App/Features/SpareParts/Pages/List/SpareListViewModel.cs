using App.Features.Accessories.Pages.Add;
using App.Features.Accessories.Services;
using App.Providers.Navigation.Services;
using App.Providers.Navigation.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using App.Features.SpareParts.Models;
using App.Providers.Database.Services;

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

        readonly ISQLiteService _sqliteService;
        readonly INavigationService _navigationService;
        readonly IAccessoriesService _accessoriesService;

        #endregion

        #region Command

        public ICommand AddIconTappedCommand { get; set; }

        #endregion

        #region Constructor

        public SpareListViewModel(ISQLiteService sqliteService, INavigationService navigationService,
                                  IAccessoriesService accessoriesService)
        {
            _sqliteService = sqliteService;
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
            var spares = await _sqliteService.GetAllItemsAsync<SparePart>();
            Spares = new ObservableCollection<SparePart>(spares);
        }

        #endregion
    }
}
