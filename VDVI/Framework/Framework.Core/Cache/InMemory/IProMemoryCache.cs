using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;

namespace Framework.Core.Cache.InMemory
{
    public interface IProMemoryCache
    {
        Task<Result<PrometheusResponse>> SetCacheAsync(object key, object value, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null);
        Task<Result<PrometheusResponse>> GetCacheAsync(object key);
    }
}