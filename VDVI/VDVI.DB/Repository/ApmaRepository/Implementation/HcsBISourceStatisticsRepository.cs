using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VDVI.DB.IRepository;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.DB.Repository
{
    public class HcsBISourceStatisticsRepository : DapperRepository<SourceStatisticDto>, IHcsBISourceStatisticsRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbSourceStatistic> _tblRepository;

        public HcsBISourceStatisticsRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.SourceStatistic;
        }
        public async Task<string> BulkInsertWithProcAsync(IEnumerable<SourceStatisticDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_SourceStatistics_History", new { SourceStatistics_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<SourceStatisticDto>> BulkInsertAsync(IEnumerable<SourceStatisticDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbSourceStatistic>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _tblRepository.DeleteAsync(x => x.BusinessDate == businessDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<SourceStatisticDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<SourceStatisticDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<SourceStatisticDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbSourceStatistic> dbEntities = await _dbContext
                .SourceStatistic
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<SourceStatisticDto>>(dbEntities);

            return entities;
        }

        public async Task<SourceStatisticDto> InsertAsync(SourceStatisticDto dto)
        {
            var dbEntity = TinyMapper.Map<DbSourceStatistic>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<SourceStatisticDto>(dbEntity);
        }

        public async Task<SourceStatisticDto> UpdateAsync(SourceStatisticDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbSourceStatistic>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
