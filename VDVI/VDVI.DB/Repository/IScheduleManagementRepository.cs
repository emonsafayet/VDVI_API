using Framework.Core.Repository;
using VDVI.Repository.Repository.Interfaces;

namespace VDVI.Repository.Repository
{
    public interface IScheduleManagementRepository : IProRepository
    {
        IRoomSummaryRepository RoomSummaryRepository { get; }
    }
}
