using System.Data.Common;
using MicroOrm.Dapper.Repositories.DbContext;

namespace Framework.Core.Repository
{
    public class ProDbContext : DapperDbContext
    {
        protected ProDbContext(DbConnection connectionString) : base(connectionString)
        {

        }
    }
}
