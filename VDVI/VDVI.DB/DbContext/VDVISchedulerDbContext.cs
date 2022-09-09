using Framework.Core.Repository;
using MicroOrm.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using VDVI.DB.DbModels.RoomSummary;
using VDVI.DB.Dtos;
using VDVI.DB.Models.Accounts;

namespace VDVI.Repository.DbContext
{

    public class VDVISchedulerDbContext : ProDbContext, IVDVISchedulerDbContext
    {
        private IDapperRepository<DbRoomSummary> _roomSummary;
        private IDapperRepository<DbLedgerBalance> _ledgerBalance;


        public VDVISchedulerDbContext(IConfiguration configuration) : base(new SqlConnection(configuration["ConnectionStrings:ApmaDb"]))
        {
            TinyMapper.Bind<RoomSummaryDto, DbRoomSummary>();
            TinyMapper.Bind<List<RoomSummaryDto>, List<DbRoomSummary>>();
            TinyMapper.Bind<DbRoomSummary, RoomSummaryDto>();
            TinyMapper.Bind<List<DbRoomSummary>, List<RoomSummaryDto>>();

            TinyMapper.Bind<LedgerBalanceDto, DbLedgerBalance>();
            TinyMapper.Bind<List<LedgerBalanceDto>, List<DbLedgerBalance>>();
            TinyMapper.Bind<DbLedgerBalance, LedgerBalanceDto>();
            TinyMapper.Bind<List<DbLedgerBalance>, List<LedgerBalanceDto>>();
        }

        public IDapperRepository<DbRoomSummary> RoomSummary => _roomSummary ??= new DapperRepository<DbRoomSummary>(Connection);

        public IDapperRepository<DbLedgerBalance> LedgerBalance => _ledgerBalance ??= new DapperRepository<DbLedgerBalance>(Connection);
    }
}
