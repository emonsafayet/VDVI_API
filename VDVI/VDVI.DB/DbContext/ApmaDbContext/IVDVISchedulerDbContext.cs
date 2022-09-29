using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.DbContext;
using VDVI.Repository.DB; 

namespace VDVI.Repository.DbContext.ApmaDbContext
{
    public interface IVDVISchedulerDbContext : IDapperDbContext
    {
        IDapperRepository<DbSchedulerSetup> SchedulerSetup { get; }
        IDapperRepository<DbSchedulerLog> SchedulerLog { get; }
        IDapperRepository<DbRoomSummaryHistory> RoomSummary { get; }
        IDapperRepository<DbLedgerBalanceHistory> LedgerBalance { get; }
        IDapperRepository<DbSourceStatisticHistory> SourceStatistic { get; }
        IDapperRepository<DbSourceStatisticFuture> SourceStatisticFuture { get; }
        IDapperRepository<DbSourceStatisticFutureAudit> SourceStatisticFutureAudit { get; }
        IDapperRepository<DbRatePlanStatisticHistory> RatePlanStatisticHistory { get; }
        IDapperRepository<DbRoomsHistory> Rooms { get; }
        IDapperRepository<DbRevenueHistory> Revenue { get; }
        IDapperRepository<DbOccupancyHistory> Occupancy { get; }
        IDapperRepository<DbReservationHistory> Reservation { get; }
        IDapperRepository<DbDailyHistory> DailyHistory { get; }
    }
}
