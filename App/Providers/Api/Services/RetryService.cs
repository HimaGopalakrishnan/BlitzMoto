using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;

namespace App.Providers.Api.Services
{
    public class RetryService : IRetryService
    {
        public async Task<T> Retry<T>(Func<Task<T>> func, int retryCount, Func<Exception, TimeSpan, Task> onRetry)
        {
            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(1);

            return await Policy.HandleInner<Exception>().WaitAndRetryAsync(new[]
         {
            TimeSpan.FromSeconds(6),
            TimeSpan.FromSeconds(12)
         }, onRetry).ExecuteAsync<T>(func);
        }
        public async Task<T> LongRetry<T>(Func<Task<T>> func, int retryCount, Func<Exception, TimeSpan, Task> onRetry)
        {
            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(1);

            return await Policy.HandleInner<Exception>().WaitAndRetryAsync(new[]
         {
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(15)
         }, onRetry).ExecuteAsync<T>(func);
        }
    }
}
