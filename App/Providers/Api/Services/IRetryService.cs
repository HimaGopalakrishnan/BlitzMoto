using System;
using System.Threading.Tasks;

namespace App.Providers.Api.Services
{
    public interface IRetryService
    {
        Task<T> Retry<T>(Func<Task<T>> func, int retryCount, Func<Exception, TimeSpan, Task> onRetry);
        Task<T> LongRetry<T>(Func<Task<T>> func, int retryCount, Func<Exception, TimeSpan, Task> onRetry);
    }
}
