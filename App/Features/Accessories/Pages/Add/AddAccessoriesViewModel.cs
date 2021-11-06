﻿using App.Features.Accessories.Models;
using App.Providers.Database.Services;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Base;
using App.Providers.Navigation.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Features.Accessories.Pages.Add
{
    public class AddAccessoriesViewModel : ViewModelBase
    {
        #region Services

        readonly ISQLiteService _sqliteService;
        readonly INavigationService _navigationService;
        readonly IDialogService _dialogService;

        #endregion

        #region Commands

        public ICommand AddCommand { get; set; }

        #endregion

        #region Properties

        string _partNumber;
        public string PartNumber
        {
            get => _partNumber;
            set => SetProperty(ref _partNumber, value);
        }

        string _accessory;
        public string Accessory
        {
            get => _accessory;
            set => SetProperty(ref _accessory, value);
        }

        int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        double _price;
        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        #endregion

        #region Constructor

        public AddAccessoriesViewModel(ISQLiteService sqliteService, INavigationService navigationService,
                                       IDialogService dialogService)
        {
            _sqliteService = sqliteService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            AddCommand = new Command(async () => await Add());
        }

        #endregion

        #region Methods

        async Task Add()
        {
            await Task.Delay(1);
            bool isValid = Validate();
            if (isValid)
            {
                var accessory = new Accessory { Number = PartNumber, Name = Accessory, Quantity = Quantity, Price = Price };
                var id = await _sqliteService.SaveItemAsync(accessory);
                if (id > 0)
                {
                    _dialogService.Toast("Added");
                    await _navigationService.NavigateToRootAsync();
                }
                else
                {
                    _dialogService.Alert("Failed");
                }
            }
            else
            {
                _dialogService.Alert("Invalid data");
            }
        }

        public bool Validate()
        {
            return true;
        }

        #endregion
    }
}