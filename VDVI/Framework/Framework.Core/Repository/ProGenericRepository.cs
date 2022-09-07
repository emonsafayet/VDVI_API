using MicroOrm.Dapper.Repositories;

namespace Framework.Core.Repository
{
    public class ProGenericRepository<T> : DapperRepository<T> where T : class
    {
        protected ProGenericRepository(ProDbContext dbContext) : base(dbContext.Connection)
        {

        }
    }
}
