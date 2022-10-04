using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.DbContext;
using VDVI.Repository.Models.AfasModel;
using VDVI.Repository.DB; 

namespace VDVI.Repository.DbContext.AfasDbContext
{
    public interface IAfasDbContext : IDapperDbContext
    {
        IDapperRepository<DbDMFAdministraties> Administraties { get; }
        IDapperRepository<DbAfasSchedulerSetup> AfasSchedulerSetup { get; }
        IDapperRepository<DbAfasSchedulerLog> AfasSchedulerLog { get; }

    }
}
