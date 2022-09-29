using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using MicroOrm.Dapper.Repositories;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DB;
using VDVI.Repository.DbContext.ApmaDbContext;
using Dapper;
using VDVI.DB.Dtos;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIRatePlanStatisticsFutureRepository : DapperRepository<DbRatePlanStatisticFuture>, IHcsBIRatePlanStatisticsFutureRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRatePlanStatisticFuture> _tblRepository;

        public HcsBIRatePlanStatisticsFutureRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RatePlanStatisticFuture;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RatePlanStatisticFutureDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_RatePlanStatistics_Future", new { RatePlanStatisticsFutureUDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RatePlanStatisticFutureDto>> BulkInsertAsync(IEnumerable<RatePlanStatisticFutureDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRatePlanStatisticFuture>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _tblRepository.DeleteAsync(x => x.BusinessDate == businessDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RatePlanStatisticFutureDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RatePlanStatisticFutureDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RatePlanStatisticFutureDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRatePlanStatisticFuture> dbEntities = await _dbContext
                .RatePlanStatisticFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RatePlanStatisticFutureDto>>(dbEntities);

            return entities;
        }

        public async Task<RatePlanStatisticFutureDto> InsertAsync(RatePlanStatisticFutureDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRatePlanStatisticFuture>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RatePlanStatisticFutureDto>(dbEntity);
        }

        public async Task<RatePlanStatisticFutureDto> UpdateAsync(RatePlanStatisticFutureDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRatePlanStatisticFuture>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
