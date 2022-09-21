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
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIOccupancyFutureAuditRepository : DapperRepository<DbOccupancyFutureAudit>, IHcsBIOccupancyFutureAuditRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbOccupancyFutureAudit> _tblRepository;

        public HcsBIOccupancyFutureAuditRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.OccupancyFutureAudit;
        }

        public async Task<IEnumerable<OccupancyFutureAuditDto>> BulkInsertAsync(IEnumerable<OccupancyFutureAuditDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbOccupancyFutureAudit>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime dashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == dashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<OccupancyFutureAuditDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<OccupancyFutureAuditDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<OccupancyFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbOccupancyFuture> dbEntities = await _dbContext
                .OccupancyFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<OccupancyFutureAuditDto>>(dbEntities);

            return entities;
        }

        public async Task<OccupancyFutureAuditDto> InsertAsync(OccupancyFutureAuditDto dto)
        {
            var dbEntity = TinyMapper.Map<DbOccupancyFutureAudit>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<OccupancyFutureAuditDto>(dbEntity);
        }

        public async Task<OccupancyFutureAuditDto> UpdateAsync(OccupancyFutureAuditDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbOccupancyFutureAudit>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
