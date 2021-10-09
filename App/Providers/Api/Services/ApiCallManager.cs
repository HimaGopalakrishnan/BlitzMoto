using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Providers.Analytics.Services;
using App.Providers.Api.Resx;
using App.Providers.Cache.Services;
using App.Providers.Dialog.Services;
using Polly.Timeout;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.Providers.Api.Services
{
    public class ApiCallManager : IApiCallManager
    {
        #region Services

        readonly IDialogService _dialogService;
        readonly IAnalyticsService _analyticsService;
        readonly ICacheService _cacheService;

        #endregion

        #region Variables

        string name;

        #endregion

        #region Constructor

        public ApiCallManager(IDialogService dialogService, IAnalyticsService analyticsService,
                              ICacheService cacheService)
        {
            _dialogService = dialogService;
            _analyticsService = analyticsService;
            _cacheService = cacheService;
        }

        #endregion

        #region Methods

        protected bool HasConnectivity()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> ExecuteCall<T>(Func<Task<T>> actualTask, Action<T> onSuccess, Action<Exception> onError, bool showBusy = true, string busyMessage = "", bool isLongCall = false, bool ignoreCache = false, string cachekey = "")
        {
            try
            {
                if (ignoreCache)
                {
                    if (!HasConnectivity())
                    {
                        _dialogService.Alert(ApiManagerResources.CheckNetworkAlert,ApiManagerResources.NoInternetAlert);
                        return false;
                    }
                    else
                    {
                        if (showBusy)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (!string.IsNullOrEmpty(busyMessage))
                                {
                                    _dialogService.ShowLoading(busyMessage);
                                }
                                else
                                {
                                    _dialogService.ShowLoading(ApiManagerResources.LoadingMessage);
                                }
                            });
                        }

                        var apiStartTime = DateTime.Now;
                        name = actualTask.Method.Name;
                        var apiResult = await actualTask();
                        var apiEndTime = DateTime.Now;

                        var apiTime = (apiEndTime - apiStartTime).TotalSeconds;
                        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                        keyValuePairs.Add(Constants.Analytics.NameKey, actualTask.Method.Name);
                        keyValuePairs.Add(Constants.Analytics.TimeKey, $"{apiTime}");
                        if (apiTime >= Constants.Api.ApiRetryCount && apiTime <= Constants.Api.ApiRetryMinCount)
                        {
                            if (isLongCall)
                                _analyticsService.TrackEvent(Constants.Analytics.LongApiCallKey, keyValuePairs);
                            else
                                _analyticsService.TrackEvent(Constants.Analytics.LongApiCallKey, keyValuePairs);
                        }
                        if (apiTime > Constants.Api.ApiRetryMinCount)
                        {
                            _analyticsService.TrackEvent(Constants.Analytics.ExtraLongApiCallKey, keyValuePairs);
                        }


                        if (apiResult != null)
                        {
                            if (showBusy)
                                _dialogService.HideLoading();

                            var result = apiResult;
                            if (result != null)
                            {
                                await _cacheService.SaveObjectInCache(cachekey, result);
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    onSuccess(result);
                                });

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (showBusy)
                                _dialogService.HideLoading();

                            return false;
                        }
                    }
                }
                else
                {
                    var result = await _cacheService.GetCachedObject<T>(cachekey);
                    if (result != null)
                    {
                        if (showBusy)
                            _dialogService.HideLoading();

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            onSuccess(result);
                        });
                    }

                    return true;
                }
            }

            catch (TimeoutRejectedException exe)
            {
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add(Constants.Analytics.NameKey, name);
                _analyticsService.TrackEvent(Constants.Analytics.TimeoutKey, keyValuePairs);
                _analyticsService.TrackError(exe, keyValuePairs);
                _dialogService.HideLoading();
                var result = await _dialogService.Confirm(ApiManagerResources.SomethingWrongAlert, ApiManagerResources.Alert, ApiManagerResources.Retry);
                if (result)
                    await ExecuteCall(actualTask, onSuccess, onError, showBusy, busyMessage, isLongCall);
                return false;
            }
            catch (KeyNotFoundException ex)
            {
                _analyticsService.TrackError(ex);
                await ExecuteCall(actualTask, onSuccess, onError, showBusy, busyMessage, isLongCall, true, cachekey);
                return false;
            }
            catch (Exception ex)
            {
                var apiException = ex as Refit.ApiException;

                if (apiException != null && apiException.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _dialogService.HideLoading();
                    await _dialogService.AlertAsync(ApiManagerResources.SessionExpiredAlertMessage, ApiManagerResources.SessionExpiredAlert);

                    MessagingCenter.Instance.Send(this, Constants.MessagingCenter.SessionExpired);

                    return false;
                }
                else
                {
                    _analyticsService.TrackError(ex);

                    _dialogService.HideLoading();
                    _dialogService.Alert(ApiManagerResources.SomethingWrongAlert);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        onError(ex);
                    });

                    return false;
                }
            }
        }

        #endregion
    }
}