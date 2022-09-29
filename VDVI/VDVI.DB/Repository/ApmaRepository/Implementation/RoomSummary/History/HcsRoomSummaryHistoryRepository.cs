using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.DB;
using VDVI.ApmaRepository.Interfaces;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsRoomSummaryHistoryRepository : DapperRepository<DbRoomSummaryHistory>, IHcsRoomSummaryHistoryRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRoomSummaryHistory> _roomSummary;

        public HcsRoomSummaryHistoryRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _roomSummary = _dbContext.RoomSummary;
        }

        public async Task<RoomSummaryHistoryDto> InsertAsync(RoomSummaryHistoryDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRoomSummaryHistory>(dto);

            await _roomSummary.InsertAsync(dbEntity);

            return TinyMapper.Map<RoomSummaryHistoryDto>(dbEntity);
        }

        public async Task<IEnumerable<RoomSummaryHistoryDto>> BulkInsertAsync(IEnumerable<RoomSummaryHistoryDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRoomSummaryHistory>>(dto);

            await _roomSummary.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RoomSummaryHistoryDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ManagementSummary_RoomSummary_History", new { ManagementSummary_RoomSummary_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<RoomSummaryHistoryDto> UpdateAsync(RoomSummaryHistoryDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRoomSummaryHistory>(dto);

            await _roomSummary.UpdateAsync(dbCustomerEntity);

            return dto;
        }

        public async Task<IEnumerable<RoomSummaryHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRoomSummaryHistory> dbEntities = await _dbContext
                .RoomSummary
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RoomSummaryHistoryDto>>(dbEntities);

            return entities;
        }

        public async Task<RoomSummaryHistoryDto> FindByIdAsync(int id)
        {
            var dbEntity = await _roomSummary.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RoomSummaryHistoryDto>(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _roomSummary.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _roomSummary.DeleteAsync(x => x.BusinessDate == businessDate);
    }
}
