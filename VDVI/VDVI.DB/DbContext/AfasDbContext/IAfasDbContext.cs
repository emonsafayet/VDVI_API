using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.DbContext; 
using VDVI.Repository.AfasDtos; 

namespace VDVI.Repository.DbContext.AfasDbContext
{
    public interface IAfasDbContext : IDapperDbContext
    {
        IDapperRepository<DbDMFAdministraties> Administraties { get; }

    }
}
