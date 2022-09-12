using Framework.Core.Repository;
using VDVI.Repository.Repository.Interfaces;

namespace VDVI.Repository.Repository.ApmaRepository
{
    public interface IScheduleManagementRepository : IProRepository
    {
        IHcsRoomSummaryRepository HcsRoomSummaryRepository { get; }
    }
}
