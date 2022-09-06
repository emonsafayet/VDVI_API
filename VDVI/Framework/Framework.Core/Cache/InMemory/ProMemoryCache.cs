using System;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Logger;
using Framework.Core.Utility;
using Microsoft.Extensions.Caching.Memory;

namespace Framework.Core.Cache.InMemory
{
    public class ProMemoryCache : IProMemoryCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IProLogger _proLogger;
        private static readonly AsyncLock AsyncLock = new AsyncLock();

        public ProMemoryCache(IMemoryCache memoryCache, IProLogger proLogger)
        {
            _memoryCache = memoryCache;
            _proLogger = proLogger;
        }

        public async Task<Result<PrometheusResponse>> SetCacheAsync(object key, object value, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = absoluteExpiration ?? DateTime.UtcNow.AddDays(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = slidingExpiration ?? TimeSpan.FromMinutes(2)
                    };

                    using (await AsyncLock.LockAsync())
                    {
                        _memoryCache.Set(key, value, cacheExpiryOptions);
                    }

                    return PrometheusResponse.Success(true, "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> GetCacheAsync(object key)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    object cachedValue;

                    using (await AsyncLock.LockAsync())
                    {
                        _memoryCache.TryGetValue(key, out cachedValue);
                    }

                    if (cachedValue == null)
                        return PrometheusResponse.Failure("No cached value found!", null, HttpStatusCode.NoContent);

                    return PrometheusResponse.Success(cachedValue, "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }
    }
}
