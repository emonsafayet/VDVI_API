﻿using Framework.Core.Repository;
using VDVI.ApmaRepository.Interfaces;

namespace VDVI.ApmaRepository
{
    public interface IMasterRepository : IProRepository
    {
        // Common
        ISchedulerSetupRepository SchedulerSetupRepository { get; }
        ISchedulerLogRepository SchedulerLogRepository { get; }

        // Accounts
        IHcsLedgerBalanceRepository HcsLedgerBalanceRepository { get; }
        IHcsBIRatePlanStatisticsHistoryRepository HcsBIRatePlanStatisticsHistoryRepository { get; }
        IHcsBIRatePlanStatisticsFutureRepository HcsBIRatePlanStatisticsFutureRepository { get; }
        IHcsBIRevenueHistoryRepository HcsBIRevenueHistoryRepository { get; }
        IHcsBIRevenueFutureRepository HcsBIRevenueFutureRepository { get; }

        // RoomSummary
        IHcsRoomSummaryRepository HcsRoomSummaryRepository { get; }
        IHcsBIOccupancyHistoryRepository HcsBIOccupancyHistoryRepository { get; }
        IHcsBIOccupancyFutureRepository HcsBIOccupancyFutureRepository { get; }
        IHcsBIRoomsHistoryRepository HcsBIRoomsHistoryRepository { get; }
        IHcsBIRoomsFutureRepository HcsBIRoomsFutureRepository { get; }
        IHcsBIReservationHistoryRepository HcsBIReservationHistoryRepository { get; }
        IHcsBIReservationFutureRepository HcsBIReservationFutureRepository { get; }

        // SourceStatistics
        IHcsBISourceStatisticsHistoryRepository HcsBISourceStatisticsHistoryRepository { get; }
        IHcsBISourceStatisticsFutureRepository HcsBISourceStatisticsFutureRepository { get; }
        IHcsBISourceStatisticsFutureAuditRepository HcsBISourceStatisticsFutureAuditRepository { get; }

}
}
