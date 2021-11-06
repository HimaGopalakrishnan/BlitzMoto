﻿using App.Features.Accessories.Models;
using App.Features.Accessories.Pages.Add;
using App.Features.Accessories.Services;
using App.Providers.Navigation.Services;
using App.Providers.Navigation.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using App.Providers.Database.Services;

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

        readonly ISQLiteService _sqliteService;
        readonly INavigationService _navigationService;
        readonly IAccessoriesService _accessoriesService;

        #endregion

        #region Command

        public ICommand AddIconTappedCommand { get; set; }

        #endregion

        #region Constructor

        public AccessoriesListViewModel(ISQLiteService sqliteService, INavigationService navigationService,
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
            var accessories = await _sqliteService.GetAllItemsAsync<Accessory>();
            Accessories = new ObservableCollection<Accessory>(accessories);
        }

        #endregion
    }
}
