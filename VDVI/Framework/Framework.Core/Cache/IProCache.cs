using Framework.Core.Cache.DistributedSql;
using Framework.Core.Cache.InMemory;

namespace Framework.Core.Cache
{
    public interface IProCache
    {
        IProMemoryCache ProMemoryCache { get; }
        IProDistributedSqlCache ProDistributedSqlCache { get; }
    }
}