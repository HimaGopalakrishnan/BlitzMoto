using System.Threading.Tasks;
using App.Features.Accessories.Services;
using App.Features.SpareParts.Services;
using App.Features.User.Services;
using App.Features.Vehicles.Services;
using App.Providers.Api.Services;
using App.Providers.Database.Services;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Services;
using Xamarin.Forms;

namespace App.Providers.Navigation.Base
{
    public class ViewModelBase : ObservableObject
    {
        #region Services

        public readonly IApiCallManager ApiCallManager;
        public readonly ISQLiteService SqliteService;
        public readonly INavigationService NavigationService;
        public readonly IDialogService DialogService;
        public readonly IUserService UserService;
        public readonly IAccessoriesService AccessoriesService;
        public readonly ISpareService SpareService;
        public readonly IVehicleService VehicleService;

        #endregion

        #region Constructor

        public ViewModelBase()
        {
            ApiCallManager = ViewModelLocator.Resolve<IApiCallManager>();
            SqliteService = ViewModelLocator.Resolve<ISQLiteService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            UserService = DependencyService.Get<IUserService>();
            AccessoriesService = ViewModelLocator.Resolve<IAccessoriesService>();
            SpareService = ViewModelLocator.Resolve<ISpareService>();
            VehicleService = ViewModelLocator.Resolve<IVehicleService>();
        }

        #endregion

        #region Virtual Methods

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        #endregion
    }
}
