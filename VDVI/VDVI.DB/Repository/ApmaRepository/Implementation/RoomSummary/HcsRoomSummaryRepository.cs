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
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DB;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsRoomSummaryRepository : DapperRepository<DbRoomSummary>, IHcsRoomSummaryRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRoomSummary> _roomSummary;

        public HcsRoomSummaryRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _roomSummary = _dbContext.RoomSummary;
        }

        public async Task<RoomSummaryDto> InsertAsync(RoomSummaryDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRoomSummary>(dto);

            await _roomSummary.InsertAsync(dbEntity);

            return TinyMapper.Map<RoomSummaryDto>(dbEntity);
        }

        public async Task<IEnumerable<RoomSummaryDto>> BulkInsertAsync(IEnumerable<RoomSummaryDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRoomSummary>>(dto);

            await _roomSummary.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RoomSummaryDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_RoomSummary", new { ManagementSummary_RoomSummary_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<RoomSummaryDto> UpdateAsync(RoomSummaryDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRoomSummary>(dto);

            await _roomSummary.UpdateAsync(dbCustomerEntity);

            return dto;
        }

        public async Task<IEnumerable<RoomSummaryDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRoomSummary> dbEntities = await _dbContext
                .RoomSummary
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RoomSummaryDto>>(dbEntities);

            return entities;
        }

        public async Task<RoomSummaryDto> FindByIdAsync(int id)
        {
            var dbEntity = await _roomSummary.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RoomSummaryDto>(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _roomSummary.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _roomSummary.DeleteAsync(x => x.BusinessDate == businessDate);
    }
}
