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
    public class HcsBIRevenueFutureAuditRepository : DapperRepository<DbRevenueFutureAudit>, IHcsBIRevenueFutureAuditRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRevenueFutureAudit> _tblRepository;

        public HcsBIRevenueFutureAuditRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RevenueFutureAudit;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RevenueFutureAuditDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Revenue_Future_Audit", new { ReservationDashboard_Revenue_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RevenueFutureAuditDto>> BulkInsertAsync(IEnumerable<RevenueFutureAuditDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRevenueFutureAudit>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RevenueFutureAuditDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RevenueFutureAuditDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RevenueFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRevenueFuture> dbEntities = await _dbContext
                .RevenueFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RevenueFutureAuditDto>>(dbEntities);

            return entities;
        }

        public async Task<RevenueFutureAuditDto> InsertAsync(RevenueFutureAuditDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRevenueFutureAudit>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RevenueFutureAuditDto>(dbEntity);
        }

        public async Task<RevenueFutureAuditDto> UpdateAsync(RevenueFutureAuditDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRevenueFutureAudit>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
