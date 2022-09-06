using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;

namespace Framework.Core.Cache.DistributedSql
{
    public interface IProDistributedSqlCache
    {
        Task<Result<PrometheusResponse>> SetCacheAsync(string key, object value, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null);
        Task<Result<PrometheusResponse>> GetCacheAsync(string key);
    }
}