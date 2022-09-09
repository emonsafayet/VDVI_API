using Framework.Core.Repository;
using VDVI.Repository.DbContext;
using VDVI.Repository.Repository.Implementation;
using VDVI.Repository.Repository.Interfaces;

namespace VDVI.Repository.Repository
{
    public class ScheduleManagementRepository : ProRepository, IScheduleManagementRepository
    {

        private readonly VDVISchedulerDbContext _dbContext;

        private IRoomSummaryRepository _roomSummaryRepository;

        public ScheduleManagementRepository(VDVISchedulerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IRoomSummaryRepository RoomSummaryRepository => _roomSummaryRepository ??= new HcsRoomSummaryRepository(_dbContext);
    }
}
