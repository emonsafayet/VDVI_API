using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DB;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIRoomsFutureRepository : DapperRepository<DbRoomsFuture>, IHcsBIRoomsFutureRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRoomsFuture> _tblRepository;

        public HcsBIRoomsFutureRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RoomsFuture;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RoomsFutureDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Rooms_History", new { ReservationDashboard_Rooms_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RoomsFutureDto>> BulkInsertAsync(IEnumerable<RoomsFutureDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRoomsFuture>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RoomsFutureDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RoomsFutureDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RoomsFutureDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRoomsFuture> dbEntities = await _dbContext
                .RoomsFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RoomsFutureDto>>(dbEntities);

            return entities;
        }

        public async Task<RoomsFutureDto> InsertAsync(RoomsFutureDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRoomsFuture>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RoomsFutureDto>(dbEntity);
        }

        public async Task<RoomsFutureDto> UpdateAsync(RoomsFutureDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRoomsFuture>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
