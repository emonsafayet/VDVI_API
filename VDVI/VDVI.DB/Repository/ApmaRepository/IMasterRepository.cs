using Framework.Core.Repository;
using VDVI.DB.IRepository;
using VDVI.Repository.Interfaces;

namespace VDVI.Repository.ApmaRepository
{
    public interface IMasterRepository : IProRepository
    {
        // Common
        IJobTaskSchedulerRepository JobTaskSchedulerRepository { get; }

        // Accounts
        IHcsLedgerBalanceRepository HcsLedgerBalanceRepository { get; }
        IHcsBIRatePlanStatisticsRepository HcsBIRatePlanStatisticsRepository { get; }
        IHcsBIRevenueRepository HcsBIRevenueRepository { get; }

        // RoomSummary
        IHcsRoomSummaryRepository HcsRoomSummaryRepository { get; }
        IHcsBIOccupancyRepository HcsBIOccupancyRepository { get; }
        IHcsBIRoomsRepository HcsBIRoomsRepository { get; }
        IHcsBIReservationRepository HcsBIReservationRepository { get; }

        // SourceStatistics
        IHcsBISourceStatisticsRepository HcsBISourceStatisticsRepository { get; }
    }
}
