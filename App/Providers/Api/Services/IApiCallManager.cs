using System;
using System.Threading.Tasks;

namespace App.Providers.Api.Services
{
    public interface IApiCallManager
    {
        Task<bool> ExecuteCall<T>(Func<Task<T>> actualTask, Action<T> onSuccess, Action<Exception> onError, bool showBusy = true, string busyMessage = "", bool isLongCall = false, bool ignoreCache = false, string cachekey = "");
    }
}
