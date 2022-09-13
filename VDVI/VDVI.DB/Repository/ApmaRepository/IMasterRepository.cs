using Framework.Core.Repository;
using VDVI.ApmaRepository.Interfaces;

namespace VDVI.ApmaRepository
{
    public interface IMasterRepository : IProRepository
    {
        // Common
        IJobTaskSchedulerRepository JobTaskSchedulerRepository { get; }

        // Accounts
        IHcsLedgerBalanceRepository HcsLedgerBalanceRepository { get; }
        IHcsBIRatePlanStatisticsHistoryRepository HcsBIRatePlanStatisticsHistoryRepository { get; }
        IHcsBIRevenueHistoryRepository HcsBIRevenueHistoryRepository { get; }

        // RoomSummary
        IHcsRoomSummaryRepository HcsRoomSummaryRepository { get; }
        IHcsBIOccupancyHistoryRepository HcsBIOccupancyHistoryRepository { get; }
        IHcsBIRoomsHistoryRepository HcsBIRoomsHistoryRepository { get; }
        IHcsBIReservationHistoryRepository HcsBIReservationHistoryRepository { get; }

        // SourceStatistics
        IHcsBISourceStatisticsHistoryRepository HcsBISourceStatisticsHistoryRepository { get; }
    }
}
