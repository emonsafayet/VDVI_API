using System;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Extensions;
using Framework.Core.Logger;
using Framework.Core.Utility;
using Microsoft.Extensions.Caching.Distributed;

namespace Framework.Core.Cache.DistributedSql
{
    public class ProDistributedSqlCache : IProDistributedSqlCache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IProLogger _proLogger;
        private static readonly AsyncLock AsyncLock = new AsyncLock();

        public ProDistributedSqlCache(IDistributedCache distributedCache, IProLogger proLogger)
        {
            _distributedCache = distributedCache;
            _proLogger = proLogger;
        }

        public async Task<Result<PrometheusResponse>> SetCacheAsync(string key, object value, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var cacheExpiryOptions = new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = absoluteExpiration ?? DateTime.UtcNow.AddDays(1),
                        SlidingExpiration = slidingExpiration ?? TimeSpan.FromDays(1)
                    };

                    using (await AsyncLock.LockAsync())
                    {
                        await _distributedCache.SetAsync(key, value.ToByteArray(), cacheExpiryOptions);
                    }

                    return PrometheusResponse.Success(true, "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {exception.GetExceptionDetailMessage()}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> GetCacheAsync(string key)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    byte[] cachedValue;

                    using (await AsyncLock.LockAsync())
                    {
                        cachedValue = await _distributedCache.GetAsync(key);
                    }

                    return cachedValue == null ?
                        PrometheusResponse.Failure("No cached value found!", null, HttpStatusCode.NoContent) :
                        PrometheusResponse.Success(cachedValue.ToObject(), "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {exception.GetExceptionDetailMessage()}"),
                    RethrowException = false
                });
        }
    }
}
