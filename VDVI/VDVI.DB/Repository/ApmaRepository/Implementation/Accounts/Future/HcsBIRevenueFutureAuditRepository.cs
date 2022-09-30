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
using VDVI.DB.Dtos;
using VDVI.Repository.DB;
using VDVI.Repository.DbContext.ApmaDbContext; 
namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIRevenueFutureAuditRepository : DapperRepository<DbReservationDashboardRevenueFutureAudit>, IHcsBIRevenueFutureAuditRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbReservationDashboardRevenueFutureAudit> _tblRepository;

        public HcsBIRevenueFutureAuditRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RevenueFutureAudit;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDashboardRevenueFutureAuditDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Revenue_Future_Audit", new { ReservationDashboard_Revenue_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<ReservationDashboardRevenueFutureAuditDto>> BulkInsertAsync(IEnumerable<ReservationDashboardRevenueFutureAuditDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbReservationDashboardRevenueFutureAudit>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<ReservationDashboardRevenueFutureAuditDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<ReservationDashboardRevenueFutureAuditDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<ReservationDashboardRevenueFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbReservationDashboardRevenueFuture> dbEntities = await _dbContext
                .RevenueFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<ReservationDashboardRevenueFutureAuditDto>>(dbEntities);

            return entities;
        }

        public async Task<ReservationDashboardRevenueFutureAuditDto> InsertAsync(ReservationDashboardRevenueFutureAuditDto dto)
        {
            var dbEntity = TinyMapper.Map<DbReservationDashboardRevenueFutureAudit>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<ReservationDashboardRevenueFutureAuditDto>(dbEntity);
        }

        public async Task<ReservationDashboardRevenueFutureAuditDto> UpdateAsync(ReservationDashboardRevenueFutureAuditDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbReservationDashboardRevenueFutureAudit>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
