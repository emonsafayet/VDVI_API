using Framework.Core.Cache.DistributedSql;
using Framework.Core.Cache.InMemory;

namespace Framework.Core.Cache
{
    public class ProCache : IProCache
    {
        public ProCache(IProMemoryCache proMemoryCache, IProDistributedSqlCache proDistributedSqlCache)
        {
            ProMemoryCache = proMemoryCache;
            ProDistributedSqlCache = proDistributedSqlCache;
        }

        public IProMemoryCache ProMemoryCache { get; }
        public IProDistributedSqlCache ProDistributedSqlCache { get; }
    }
}
