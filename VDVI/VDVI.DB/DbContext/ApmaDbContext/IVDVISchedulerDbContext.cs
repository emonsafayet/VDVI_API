using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.DbContext;
using VDVI.Repository.DB;
using VDVI.Repository.Dtos.ApmaDtos.Common;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.Repository.DbContext.ApmaDbContext
{
    public interface IVDVISchedulerDbContext : IDapperDbContext
    {
        IDapperRepository<DbJobTaskScheduler> JobTaskScheduler { get; }
        IDapperRepository<DbSchedulerSetup> SchedulerSetup { get; }
        IDapperRepository<DbSchedulerLog> SchedulerLog { get; }
        IDapperRepository<DbRoomSummary> RoomSummary { get; }
        IDapperRepository<DbLedgerBalance> LedgerBalance { get; }
        IDapperRepository<DbSourceStatisticHistory> SourceStatistic { get; }
        IDapperRepository<DbSourceStatisticFuture> SourceStatisticFuture { get; }
        IDapperRepository<DbRatePlanStatisticHistory> RatePlanStatisticHistory { get; }
        IDapperRepository<DbRoomsHistory> Rooms { get; }
        IDapperRepository<DbRevenueHistory> Revenue { get; }
        IDapperRepository<DbOccupancyHistory> Occupancy { get; }
        IDapperRepository<DbReservationHistory> Reservation { get; }
    }
}
