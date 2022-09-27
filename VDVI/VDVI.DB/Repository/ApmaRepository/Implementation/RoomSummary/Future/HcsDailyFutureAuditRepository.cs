using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VDVI.Repository.DbContext.ApmaDbContext; 
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DB;
using VDVI.DB.Dtos;
using VDVI.Repository.Models.ApmaModels.RoomSummary;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsDailyFutureAuditRepository : DapperRepository<DbDailyHistory>, IHcsDailyFutureAuditRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbDailyHistory> _tblRepository;

        public HcsDailyFutureAuditRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.DailyHistory;
        }

        public async Task<IEnumerable<DailyFutureAuditDto>> BulkInsertAsync(IEnumerable<DailyFutureAuditDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbDailyHistory>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.Date == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<DailyFutureAuditDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<DailyFutureAuditDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<DailyFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbDailyHistory> dbEntities = await _dbContext
                .DailyHistory
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<DailyFutureAuditDto>>(dbEntities);

            return entities;
        }

        public async Task<DailyFutureAuditDto> InsertAsync(DailyFutureAuditDto dto)
        {
            var dbEntity = TinyMapper.Map<DbDailyHistory>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<DailyFutureAuditDto>(dbEntity);
        }

        public async Task<DailyFutureAuditDto> UpdateAsync(DailyFutureAuditDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbDailyHistory>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
