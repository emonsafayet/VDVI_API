using Framework.Core.Repository;
using MicroOrm.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VDVI.DB.DbModels.RoomSummary;
using VDVI.DB.Dtos;
using VDVI.DB.Models.Accounts;
using VDVI.DB.Models.Common;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.RoomSummary;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.Repository.DbContext.ApmaDbContext
{

    public class VDVISchedulerDbContext : ProDbContext, IVDVISchedulerDbContext
    {
        private IDapperRepository<DbJobTaskScheduler> _taskScheduler;
        private IDapperRepository<DbRoomSummary> _roomSummary;
        private IDapperRepository<DbLedgerBalance> _ledgerBalance;
        private IDapperRepository<DbRatePlanStatistic> _ratePlanStatistic;
        private IDapperRepository<DbSourceStatistic> _sourceStatistic;
        private IDapperRepository<DbOccupancy> _occupancy;
        private IDapperRepository<DbReservation> _reservation;
        private IDapperRepository<DbRooms> _rooms;
        private IDapperRepository<DbRevenue> _revenue;


        public VDVISchedulerDbContext(IConfiguration configuration) : base(new SqlConnection(configuration["ConnectionStrings:ApmaDb"]))
        {
            // Dto to Db - Single
            TinyMapper.Bind<JobTaskSchedulerDto, DbJobTaskScheduler>();
            TinyMapper.Bind<RoomSummaryDto, DbRoomSummary>();
            TinyMapper.Bind<LedgerBalanceDto, DbLedgerBalance>();
            TinyMapper.Bind<RatePlanStatisticDto, DbRatePlanStatistic>();
            TinyMapper.Bind<SourceStatisticDto, DbSourceStatistic>();
            TinyMapper.Bind<OccupancyDto, DbOccupancy>();
            TinyMapper.Bind<ReservationDto, DbReservation>();
            TinyMapper.Bind<RoomsDto, DbRooms>();
            TinyMapper.Bind<RevenueDto, DbRevenue>();

            // Dto to Db List
            TinyMapper.Bind<List<JobTaskSchedulerDto>, List<DbJobTaskScheduler>>();
            TinyMapper.Bind<List<RoomSummaryDto>, List<DbRoomSummary>>();
            TinyMapper.Bind<List<LedgerBalanceDto>, List<DbLedgerBalance>>();
            TinyMapper.Bind<List<RatePlanStatisticDto>, List<DbRatePlanStatistic>>();
            TinyMapper.Bind<List<SourceStatisticDto>, List<DbSourceStatistic>>();
            TinyMapper.Bind<List<OccupancyDto>, List<DbOccupancy>>();
            TinyMapper.Bind<List<ReservationDto>, List<DbReservation>>();
            TinyMapper.Bind<List<RoomsDto>, List<DbRooms>>();
            TinyMapper.Bind<List<RevenueDto>, List<DbRevenue>>();
        }

        public IDapperRepository<DbJobTaskScheduler> JobTaskScheduler => _taskScheduler ??= new DapperRepository<DbJobTaskScheduler>(Connection);
        public IDapperRepository<DbRoomSummary> RoomSummary => _roomSummary ??= new DapperRepository<DbRoomSummary>(Connection);
        public IDapperRepository<DbLedgerBalance> LedgerBalance => _ledgerBalance ??= new DapperRepository<DbLedgerBalance>(Connection);
        public IDapperRepository<DbRatePlanStatistic> RatePlanStatistic => _ratePlanStatistic ??= new DapperRepository<DbRatePlanStatistic>(Connection);
        public IDapperRepository<DbSourceStatistic> SourceStatistic => _sourceStatistic ??= new DapperRepository<DbSourceStatistic>(Connection);
        public IDapperRepository<DbOccupancy> Occupancy => _occupancy ??= new DapperRepository<DbOccupancy>(Connection);
        public IDapperRepository<DbReservation> Reservation => _reservation ??= new DapperRepository<DbReservation>(Connection);
        public IDapperRepository<DbRooms> Rooms => _rooms ??= new DapperRepository<DbRooms>(Connection);
        public IDapperRepository<DbRevenue> Revenue => _revenue ??= new DapperRepository<DbRevenue>(Connection);
    }
}
