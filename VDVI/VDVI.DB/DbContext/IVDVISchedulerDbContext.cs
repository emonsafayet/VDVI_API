using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.DbContext;
using VDVI.DB.DbModels.RoomSummary;

namespace VDVI.Repository.DbContext
{
    public interface IVDVISchedulerDbContext : IDapperDbContext
    {
        IDapperRepository<DbRoomSummary> RoomSummary { get; }
    }
}
