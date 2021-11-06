using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Xamarin.Essentials;
using Refit;
using App.Providers.Dialog.Services;
using App.Providers.Analytics.Services;
using App.Providers.Api.Services;
using App.Providers.Cache.Services;
using App.Constants;
using App.Providers.Navigation.Services;
using App.Features.Menu.Pages;
using App.Features.Accessories.Services;
using App.Features.Accessories.Pages.List;
using App.Features.Accessories.Pages.Add;
using Providers.Navigation.Services;
using App.Features.Api;
using App.Providers.Database.Services;

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

            services.AddTransient<AccessoriesListViewModel>();
            services.AddTransient<AddAccessoriesViewModel>();
            services.AddTransient<MenuViewModel>();

            #endregion

            #region Providers

            services.AddTransient<ISQLiteService, SQLiteService>();
            services.AddTransient<INavigationService, NavigationService>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<IAnalyticsService, AnalyticsService>();
            services.AddTransient<IApiCallManager, ApiCallManager>();
            services.AddTransient<IRetryService, RetryService>();
            services.AddTransient<ICacheService, CacheService>();

            #endregion

            #region Services

            services.AddTransient<IAccessoriesService, AccessoriesService>();

            #endregion

            #region Refit

            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            var isDevelopment = string.Equals(environment, "Development");

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(Api.ApiTimeOut);

            services.AddRefitClient<IBlitzApi>()
                    .AddPolicyHandler(timeoutPolicy)
                    .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri(isDevelopment ? Api.LocalApiEndpoint : GlobalSetting.DefaultEndpoint);
                    });

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
            services.AddHttpClient(Api.ApiClientName);
        }

        #endregion
    }
}
