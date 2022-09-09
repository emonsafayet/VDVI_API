using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.DbContext;
using VDVI.DB.DbModels.RoomSummary;
using VDVI.DB.Models.Accounts;

namespace VDVI.Repository.DbContext
{
    public interface IVDVISchedulerDbContext : IDapperDbContext
    {
        IDapperRepository<DbRoomSummary> RoomSummary { get; }
        IDapperRepository<DbLedgerBalance> LedgerBalance { get; }
    }
}
