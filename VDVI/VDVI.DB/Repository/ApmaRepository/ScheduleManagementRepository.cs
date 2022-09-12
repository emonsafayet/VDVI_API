using Framework.Core.Repository;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Repository.Implementation;
using VDVI.Repository.Repository.Interfaces;

namespace VDVI.Repository.Repository.ApmaRepository
{
    public class ScheduleManagementRepository : ProRepository, IScheduleManagementRepository
    {

        private readonly VDVISchedulerDbContext _dbContext;

        private IHcsRoomSummaryRepository _hcsRoomSummaryRepository;
        private IHcsLedgerBalanceRepository _hcsLedgerBalanceRepository;


        public ScheduleManagementRepository(VDVISchedulerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IHcsRoomSummaryRepository HcsRoomSummaryRepository => _hcsRoomSummaryRepository ??= new HcsRoomSummaryRepository(_dbContext);
        public IHcsLedgerBalanceRepository HcsLedgerBalanceRepository => _hcsLedgerBalanceRepository ??= new HcsLedgerBalanceRepository(_dbContext);

    }
}
