﻿using Framework.Core.Repository;
using MicroOrm.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using VDVI.DB.Dtos;
using VDVI.Repository.DB;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.ApmaDtos.Common;
using VDVI.Repository.Dtos.RoomSummary;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.Repository.DbContext.ApmaDbContext
{

    public class VDVISchedulerDbContext : ProDbContext, IVDVISchedulerDbContext
    {
        private IDapperRepository<DbSchedulerSetup> _schedulerSetup; 
        private IDapperRepository<DbSchedulerLog> _schedulerlog;

        private IDapperRepository<DbRoomSummary> _roomSummary;
        private IDapperRepository<DbLedgerBalance> _ledgerBalance;

        private IDapperRepository<DbRatePlanStatisticHistory> _ratePlanStatistic;
        private IDapperRepository<DbSourceStatisticHistory> _sourceStatistic;
        private IDapperRepository<DbOccupancyHistory> _occupancy;
        private IDapperRepository<DbReservationHistory> _reservation;
        private IDapperRepository<DbRoomsHistory> _rooms;
        private IDapperRepository<DbRevenueHistory> _revenue;
        private IDapperRepository<DbSourceStatisticFuture> _sourceStatisticFuture;
        private IDapperRepository<DbSourceStatisticFutureAudit> _sourceStatisticFutureAudit;
        private IDapperRepository<DbRatePlanStatisticFuture> _ratePlanStatisticFuture;
        private IDapperRepository<DbRevenueFuture> _revenueFuture;
        private IDapperRepository<DbRoomsFuture> _roomsFuture;
        private IDapperRepository<DbReservationFuture> _reservationFuture;
        private IDapperRepository<DbOccupancyFuture> _occupancyFuture;
        private IDapperRepository<DbRatePlanStatisticFutureAudit> _ratePlanStatisticFutureAudit;
        private IDapperRepository<DbRoomsFutureAudit> _roomsFutureAudit;
        private IDapperRepository<DbOccupancyFutureAudit> _occupancyFutureAudit;
        private IDapperRepository<DbReservationFutureAudit> _reservationFutureAudit;
        private IDapperRepository<DbRevenueFutureAudit> _revenueFutureAudit;

        public VDVISchedulerDbContext(IConfiguration configuration) : base(new SqlConnection(configuration["ConnectionStrings:ApmaDb"]))
        {
            // Dto to Db - Single
            TinyMapper.Bind<SchedulerSetupDto, DbSchedulerSetup>();
            TinyMapper.Bind<SchedulerLogDto, DbSchedulerLog>();
            TinyMapper.Bind<RoomSummaryDto, DbRoomSummary>();
            TinyMapper.Bind<LedgerBalanceDto, DbLedgerBalance>();
            TinyMapper.Bind<RatePlanStatisticHistoryDto, DbRatePlanStatisticHistory>();
            TinyMapper.Bind<SourceStatisticHistoryDto, DbSourceStatisticHistory>();
            TinyMapper.Bind<OccupancyHistoryDto, DbOccupancyHistory>();
            TinyMapper.Bind<ReservationHistoryDto, DbReservationHistory>();
            TinyMapper.Bind<RoomsHistoryDto, DbRoomsHistory>();
            TinyMapper.Bind<RevenueHistoryDto, DbRevenueHistory>();
            TinyMapper.Bind<SourceStatisticFutureDto, DbSourceStatisticFuture>();
            TinyMapper.Bind<SourceStatisticsFutureAuditDto, DbSourceStatisticFutureAudit>();
            TinyMapper.Bind<OccupancyFutureDto, DbOccupancyFuture>();
            TinyMapper.Bind<ReservationFutureDto, DbReservationFuture>();
            TinyMapper.Bind<RoomsFutureDto, DbRoomsFuture>();
            TinyMapper.Bind<RevenueFutureDto, DbRevenueFuture>();
            TinyMapper.Bind<RatePlanStatisticFutureDto, DbRatePlanStatisticFuture>();
            TinyMapper.Bind<RatePlanStatisticFutureAuditDto, DbRatePlanStatisticFutureAudit>();
            TinyMapper.Bind<RoomsFutureAuditDto, DbRoomsFutureAudit>();
            TinyMapper.Bind<OccupancyFutureAuditDto, DbOccupancyFutureAudit>();
            TinyMapper.Bind<ReservationFutureAuditDto, DbReservationFutureAudit>();
            TinyMapper.Bind<RevenueFutureAuditDto, DbRevenueFutureAudit>();

        // Dto to Db List
        TinyMapper.Bind<List<SchedulerLogDto>, List<DbSchedulerLog>>();
            TinyMapper.Bind<List<SchedulerSetupDto>,List<DbSchedulerSetup>>();
            TinyMapper.Bind<List<RoomSummaryDto>, List<DbRoomSummary>>();
            TinyMapper.Bind<List<LedgerBalanceDto>, List<DbLedgerBalance>>();
            TinyMapper.Bind<List<RatePlanStatisticHistoryDto>, List<DbRatePlanStatisticHistory>>();
            TinyMapper.Bind<List<SourceStatisticHistoryDto>, List<DbSourceStatisticHistory>>();
            TinyMapper.Bind<List<OccupancyHistoryDto>, List<DbOccupancyHistory>>();
            TinyMapper.Bind<List<ReservationHistoryDto>, List<DbReservationHistory>>();
            TinyMapper.Bind<List<RoomsHistoryDto>, List<DbRoomsHistory>>();
            TinyMapper.Bind<List<RevenueHistoryDto>, List<DbRevenueHistory>>();
            TinyMapper.Bind<List<SourceStatisticFutureDto>, List<DbSourceStatisticFuture>>();
            TinyMapper.Bind<List<SourceStatisticsFutureAuditDto>,List<DbSourceStatisticFutureAudit>>();
            TinyMapper.Bind<List<OccupancyFutureDto>, List<DbOccupancyFuture>>();
            TinyMapper.Bind<List<ReservationFutureDto>, List<DbReservationFuture>>();
            TinyMapper.Bind<List<RoomsFutureDto>, List<DbRoomsFuture>>();
            TinyMapper.Bind<List<RevenueFutureDto>, List<DbRevenueFuture>>();
            TinyMapper.Bind<List<RatePlanStatisticFutureDto>, List<DbRatePlanStatisticFuture>>();
            TinyMapper.Bind<List<RatePlanStatisticFutureAuditDto>, List<DbRatePlanStatisticFutureAudit>>();
            TinyMapper.Bind<List<RoomsFutureAuditDto>, List<DbRoomsFutureAudit>>();
            TinyMapper.Bind<List<OccupancyFutureAuditDto>, List<DbOccupancyFutureAudit>>();
            TinyMapper.Bind<List<ReservationFutureAuditDto>, List<DbReservationFutureAudit>>();
            TinyMapper.Bind<List<RevenueFutureAuditDto>, List<DbRevenueFutureAudit>>();
        }
        public IDapperRepository<DbSchedulerSetup> SchedulerSetup => _schedulerSetup ??= new DapperRepository<DbSchedulerSetup>(Connection);
        public IDapperRepository<DbSchedulerLog> SchedulerLog => _schedulerlog ??= new DapperRepository<DbSchedulerLog>(Connection);
        public IDapperRepository<DbRoomSummary> RoomSummary => _roomSummary ??= new DapperRepository<DbRoomSummary>(Connection);
        public IDapperRepository<DbLedgerBalance> LedgerBalance => _ledgerBalance ??= new DapperRepository<DbLedgerBalance>(Connection);
        public IDapperRepository<DbRatePlanStatisticHistory> RatePlanStatisticHistory => _ratePlanStatistic ??= new DapperRepository<DbRatePlanStatisticHistory>(Connection);
        public IDapperRepository<DbRatePlanStatisticFuture> RatePlanStatisticFuture => _ratePlanStatisticFuture ??= new DapperRepository<DbRatePlanStatisticFuture>(Connection);
        public IDapperRepository<DbSourceStatisticHistory> SourceStatistic => _sourceStatistic ??= new DapperRepository<DbSourceStatisticHistory>(Connection);
        public IDapperRepository<DbOccupancyHistory> Occupancy => _occupancy ??= new DapperRepository<DbOccupancyHistory>(Connection);
        public IDapperRepository<DbReservationHistory> Reservation => _reservation ??= new DapperRepository<DbReservationHistory>(Connection);
        public IDapperRepository<DbRoomsHistory> Rooms => _rooms ??= new DapperRepository<DbRoomsHistory>(Connection);
        public IDapperRepository<DbRevenueHistory> Revenue => _revenue ??= new DapperRepository<DbRevenueHistory>(Connection);
        public IDapperRepository<DbSourceStatisticFuture> SourceStatisticFuture => _sourceStatisticFuture ??= new DapperRepository<DbSourceStatisticFuture>(Connection);
        public IDapperRepository<DbSourceStatisticFutureAudit> SourceStatisticFutureAudit => _sourceStatisticFutureAudit ??= new DapperRepository<DbSourceStatisticFutureAudit>(Connection);
        public IDapperRepository<DbRevenueFuture> RevenueFuture => _revenueFuture ??= new DapperRepository<DbRevenueFuture>(Connection);
        public IDapperRepository<DbRoomsFuture> RoomsFuture => _roomsFuture ??= new DapperRepository<DbRoomsFuture>(Connection);
        public IDapperRepository<DbReservationFuture> ReservationFuture => _reservationFuture ??= new DapperRepository<DbReservationFuture>(Connection);
        public IDapperRepository<DbOccupancyFuture> OccupancyFuture => _occupancyFuture ??= new DapperRepository<DbOccupancyFuture>(Connection);
        public IDapperRepository<DbRatePlanStatisticFutureAudit> RatePlanStatisticFutureAudit => _ratePlanStatisticFutureAudit ??= new DapperRepository<DbRatePlanStatisticFutureAudit>(Connection);
        public IDapperRepository<DbRoomsFutureAudit> RoomsFutureAudit => _roomsFutureAudit ??= new DapperRepository<DbRoomsFutureAudit>(Connection);
        public IDapperRepository<DbOccupancyFutureAudit> OccupancyFutureAudit => _occupancyFutureAudit ??= new DapperRepository<DbOccupancyFutureAudit>(Connection);
        public IDapperRepository<DbReservationFutureAudit> ReservationFutureAudit => _reservationFutureAudit ??= new DapperRepository<DbReservationFutureAudit>(Connection);
        public IDapperRepository<DbRevenueFutureAudit> RevenueFutureAudit => _revenueFutureAudit ??= new DapperRepository<DbRevenueFutureAudit>(Connection);
    }
}
