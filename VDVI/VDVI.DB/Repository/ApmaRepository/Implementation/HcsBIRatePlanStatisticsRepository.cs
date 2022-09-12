using System;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using VDVI.Repository.Interfaces;
using VDVI.Repository.Dtos.Accounts;
using System.Threading.Tasks;
using MicroOrm.Dapper.Repositories;
using VDVI.Repository.DbContext.ApmaDbContext;
using Dapper;
using Nelibur.ObjectMapper;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;

namespace VDVI.Repository.Implementation
{
    public class HcsBIRatePlanStatisticsRepository : DapperRepository<DbRatePlanStatistic>, IHcsBIRatePlanStatisticsRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRatePlanStatistic> _tblRepository;

        public HcsBIRatePlanStatisticsRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RatePlanStatistic;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RatePlanStatisticDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_RatePlanStatistics_History", new { RatePlanStatistics_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RatePlanStatisticDto>> BulkInsertAsync(IEnumerable<RatePlanStatisticDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRatePlanStatistic>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _tblRepository.DeleteAsync(x => x.BusinessDate == businessDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RatePlanStatisticDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RatePlanStatisticDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RatePlanStatisticDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRatePlanStatistic> dbEntities = await _dbContext
                .RatePlanStatistic
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RatePlanStatisticDto>>(dbEntities);

            return entities;
        }

        public async Task<RatePlanStatisticDto> InsertAsync(RatePlanStatisticDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRatePlanStatistic>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RatePlanStatisticDto>(dbEntity);
        }

        public async Task<RatePlanStatisticDto> UpdateAsync(RatePlanStatisticDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRatePlanStatistic>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
