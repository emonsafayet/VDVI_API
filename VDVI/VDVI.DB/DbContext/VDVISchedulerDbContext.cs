using Framework.Core.Repository;
using MicroOrm.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using VDVI.DB.DbModels.RoomSummary;
using VDVI.DB.Dtos.RoomSummary;

namespace VDVI.Repository.DbContext
{

    public class VDVISchedulerDbContext : ProDbContext, IVDVISchedulerDbContext
    {
        private IDapperRepository<DbRoomSummary> _roomSummary;


        public VDVISchedulerDbContext(IConfiguration configuration) : base(new SqlConnection(configuration["ConnectionStrings:ApmaDb"]))
        {
            TinyMapper.Bind<RoomSummaryDto, DbRoomSummary>();
            TinyMapper.Bind<List<RoomSummaryDto>, List<DbRoomSummary>>();
            TinyMapper.Bind<DbRoomSummary, RoomSummaryDto>();
            TinyMapper.Bind<List<DbRoomSummary>, List<RoomSummaryDto>>();
        }

        public IDapperRepository<DbRoomSummary> RoomSummary => _roomSummary ??= new DapperRepository<DbRoomSummary>(Connection);
    }
}
