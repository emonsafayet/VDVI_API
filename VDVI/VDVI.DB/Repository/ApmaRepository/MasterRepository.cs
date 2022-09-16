using Framework.Core.Repository;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.ApmaRepository.Implementation;
using VDVI.ApmaRepository.Interfaces;

namespace VDVI.ApmaRepository
{
    public class MasterRepository : ProRepository, IMasterRepository
    {

        private readonly VDVISchedulerDbContext _dbContext;

        public MasterRepository(VDVISchedulerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Common
        public ISchedulerSetupRepository SchedulerSetupRepository => new SchedulerSetupRepository(_dbContext);
        public ISchedulerLogRepository SchedulerLogRepository => new SchedulerLogRepository(_dbContext);

        // Accounts
        public IHcsLedgerBalanceRepository HcsLedgerBalanceRepository => new HcsLedgerBalanceRepository(_dbContext);
        public IHcsBIRevenueHistoryRepository HcsBIRevenueHistoryRepository => new HcsBIRevenueHistoryRepository(_dbContext);
        public IHcsBIRatePlanStatisticsHistoryRepository HcsBIRatePlanStatisticsHistoryRepository => new HcsBIRatePlanStatisticsHistoryRepository(_dbContext);

        // RoomSummary
        public IHcsRoomSummaryRepository HcsRoomSummaryRepository => new HcsRoomSummaryRepository(_dbContext);

        public IHcsBIOccupancyHistoryRepository HcsBIOccupancyHistoryRepository => new HcsBIOccupancyHistoryRepository(_dbContext);

        public IHcsBIRoomsHistoryRepository HcsBIRoomsHistoryRepository => new HcsBIRoomsHistoryRepository(_dbContext);

        public IHcsBIReservationHistoryRepository HcsBIReservationHistoryRepository => new HcsBIReservationHistoryRepository(_dbContext);

        // SourceStatistics
        public IHcsBISourceStatisticsHistoryRepository HcsBISourceStatisticsHistoryRepository => new HcsBISourceStatisticsHistoryRepository(_dbContext);

        public IHcsBISourceStatisticsFutureRepository HcsBISourceStatisticsFutureRepository => new HcsBISourceStatisticsFutureRepository(_dbContext);

        public IHcsBISourceStatisticsFutureAuditRepository HcsBISourceStatisticsFutureAuditRepository => new HcsBISourceStatisticsFutureAuditRepository(_dbContext);
    }
}