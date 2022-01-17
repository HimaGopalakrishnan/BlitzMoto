using App.Features.Accessories.Pages.List;
using App.Features.Accessories.Pages.Add;
using App.Features.Accessories.Services;
using App.Features.Home;
using App.Features.Menu.Pages;
using App.Features.SpareParts.Services;
using App.Providers.Analytics.Services;
using App.Providers.Api.Services;
using App.Providers.Cache.Services;
using App.Providers.Database.Services;
using App.Providers.Dialog.Services;
using App.Providers.Navigation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Providers.Navigation.Services;
using System;
using Xamarin.Essentials;
using App.Features.Vehicles.Services;
using App.Features.User.Pages.Login;
using App.Features.SpareParts.Pages.Add;
using App.Features.SpareParts.Pages.List;
using App.Features.Vehicles.Pages.Detail;
using App.Features.User.Pages.Register;
using App.Features.Vehicles.Pages.Add;
using App.Features.Vehicles.Pages.List;

namespace App
{
    public static class Startup
    {
        #region Properties

        public static bool IsMock { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }

        #endregion

        #region Methods

        public static void Init(bool isMock = false)
        {
            IsMock = isMock;

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    if (!IsMock)
                    {
                        // Tell the host configuration where to file the file (this is required for Xamarin apps)
                        c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    }
                })
                .ConfigureServices(ConfigureServices)
                .Build();

            ServiceProvider = host.Services;
        }


        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            #region Features/ToDo

            services.AddTransient<MenuViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<AccessoriesListViewModel>();
            services.AddTransient<AddAccessoriesViewModel>();
            services.AddTransient<SpareListViewModel>();
            services.AddTransient<AddSpareViewModel>();
            services.AddTransient<VehicleListViewModel>();
            services.AddTransient<VehicleDetailsViewModel>();
            services.AddTransient<AddVehicleViewModel>();

            #endregion

            #region Providers

            services.AddTransient<ISQLiteService, SQLiteService>();
            services.AddTransient<INavigationService, NavigationService>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<IAnalyticsService, AnalyticsService>();
            services.AddTransient<IApiCallManager, ApiCallManager>();
            services.AddTransient<IRetryService, RetryService>();
            services.AddTransient<ICacheService, CacheService>();

            services.AddSingleton<IAccessoriesService, AccessoriesService>();
            services.AddSingleton<ISpareService, SpareService>();
            services.AddSingleton<IVehicleService, VehicleService>();

            #endregion

            #region Services

            services.AddTransient<IAccessoriesService, AccessoriesService>();

            #endregion

            #region Mock services

            if (IsMock)
            {
                //register mock service classes here
                services.AddTransient<ICacheService, MockCacheService>();
                services.AddTransient<IAnalyticsService, MockAnalyticsService>();
            }

            #endregion

            services.AddAutoMapper(typeof(Startup));
        }

        #endregion
    }
}
