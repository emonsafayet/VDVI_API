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
    public class HcsBIRatePlanStatisticsFutureAuditRepository : DapperRepository<DbRatePlanStatisticFutureAudit>, IHcsBIRatePlanStatisticsFutureAuditRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRatePlanStatisticFutureAudit> _tblRepository;

        public HcsBIRatePlanStatisticsFutureAuditRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RatePlanStatisticFutureAudit;
        }

        public async Task<IEnumerable<RatePlanStatisticFutureAuditDto>> BulkInsertAsync(IEnumerable<RatePlanStatisticFutureAuditDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRatePlanStatisticFutureAudit>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _tblRepository.DeleteAsync(x => x.BusinessDate == businessDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RatePlanStatisticFutureAuditDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RatePlanStatisticFutureAuditDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RatePlanStatisticFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRatePlanStatisticFuture> dbEntities = await _dbContext
                .RatePlanStatisticFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RatePlanStatisticFutureAuditDto>>(dbEntities);

            return entities;
        }

        public async Task<RatePlanStatisticFutureAuditDto> InsertAsync(RatePlanStatisticFutureAuditDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRatePlanStatisticFutureAudit>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RatePlanStatisticFutureAuditDto>(dbEntity);
        }

        public async Task<RatePlanStatisticFutureAuditDto> UpdateAsync(RatePlanStatisticFutureAuditDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRatePlanStatisticFutureAudit>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
