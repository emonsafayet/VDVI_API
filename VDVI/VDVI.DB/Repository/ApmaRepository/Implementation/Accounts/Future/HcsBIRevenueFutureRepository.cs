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
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIRevenueFutureRepository : DapperRepository<DbRevenueFuture>, IHcsBIRevenueFutureRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRevenueFuture> _tblRepository;

        public HcsBIRevenueFutureRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RevenueFuture;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RevenueFutureDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Revenue_Future", new { ReservationDashboardRevenueFutureUDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RevenueFutureDto>> BulkInsertAsync(IEnumerable<RevenueFutureDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRevenueFuture>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RevenueFutureDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RevenueFutureDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RevenueFutureDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRevenueFuture> dbEntities = await _dbContext
                .RevenueFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RevenueFutureDto>>(dbEntities);

            return entities;
        }

        public async Task<RevenueFutureDto> InsertAsync(RevenueFutureDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRevenueFuture>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RevenueFutureDto>(dbEntity);
        }

        public async Task<RevenueFutureDto> UpdateAsync(RevenueFutureDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRevenueFuture>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
