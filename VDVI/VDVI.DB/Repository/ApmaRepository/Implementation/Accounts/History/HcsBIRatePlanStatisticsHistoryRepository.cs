using System;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic; 
using System.Threading.Tasks;
using MicroOrm.Dapper.Repositories;
using VDVI.Repository.DbContext.ApmaDbContext;
using Dapper;
using Nelibur.ObjectMapper;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters; 
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DB;
using VDVI.DB.Dtos;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIRatePlanStatisticsHistoryRepository : DapperRepository<DbRatePlanStatisticHistory>, IHcsBIRatePlanStatisticsHistoryRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRatePlanStatisticHistory> _tblRepository;

        public HcsBIRatePlanStatisticsHistoryRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RatePlanStatisticHistory;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RatePlanStatisticHistoryDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_RatePlanStatistics_History", new { RatePlanStatistics_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RatePlanStatisticHistoryDto>> BulkInsertAsync(IEnumerable<RatePlanStatisticHistoryDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRatePlanStatisticHistory>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _tblRepository.DeleteAsync(x => x.BusinessDate == businessDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RatePlanStatisticHistoryDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RatePlanStatisticHistoryDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RatePlanStatisticHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRatePlanStatisticHistory> dbEntities = await _dbContext
                .RatePlanStatisticHistory
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RatePlanStatisticHistoryDto>>(dbEntities);

            return entities;
        }

        public async Task<RatePlanStatisticHistoryDto> InsertAsync(RatePlanStatisticHistoryDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRatePlanStatisticHistory>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RatePlanStatisticHistoryDto>(dbEntity);
        }

        public async Task<RatePlanStatisticHistoryDto> UpdateAsync(RatePlanStatisticHistoryDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRatePlanStatisticHistory>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
