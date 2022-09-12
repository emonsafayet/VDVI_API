using Framework.Core.Repository;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Repository.Implementation;
using VDVI.Repository.Interfaces;
using VDVI.Repository.Implementation;
using VDVI.DB.IRepository;
using VDVI.DB.Repository;

namespace VDVI.Repository.ApmaRepository
{
    public class MasterRepository : ProRepository, IMasterRepository
    {

        private readonly VDVISchedulerDbContext _dbContext;

        public MasterRepository(VDVISchedulerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IJobTaskSchedulerRepository JobTaskSchedulerRepository => new JobTaskSchedulerRepository(_dbContext);

        public IHcsRoomSummaryRepository HcsRoomSummaryRepository => new HcsRoomSummaryRepository(_dbContext);

        public IHcsLedgerBalanceRepository HcsLedgerBalanceRepository => new HcsLedgerBalanceRepository(_dbContext);

        public IHcsBIRatePlanStatisticsRepository HcsBIRatePlanStatisticsRepository => new HcsBIRatePlanStatisticsRepository(_dbContext);

        public IHcsBIRevenueRepository HcsBIRevenueRepository => new HcsBIRevenueRepository(_dbContext);

        public IHcsBIOccupancyRepository HcsBIOccupancyRepository => new HcsBIOccupancyRepository(_dbContext);

        public IHcsBIRoomsRepository HcsBIRoomsRepository => new HcsBIRoomsRepository(_dbContext);

        public IHcsBISourceStatisticsRepository HcsBISourceStatisticsRepository => new HcsBISourceStatisticsRepository(_dbContext);

        public IHcsBIReservationRepository HcsBIReservationRepository => new HcsBIReservationRepository(_dbContext);
    }
}
