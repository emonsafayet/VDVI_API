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
    public class HcsBIOccupancyFutureAuditRepository : DapperRepository<DbReservationDashboardOccupancyFutureAudit>, IHcsBIOccupancyFutureAuditRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbReservationDashboardOccupancyFutureAudit> _tblRepository;

        public HcsBIOccupancyFutureAuditRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.OccupancyFutureAudit;
        }

        public async Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDashboardOccupancyFutureAuditDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Occupancy_Future_Audit", new { ReservationDashboard_Occupancy_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<ReservationDashboardOccupancyFutureAuditDto>> BulkInsertAsync(IEnumerable<ReservationDashboardOccupancyFutureAuditDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbReservationDashboardOccupancyFutureAudit>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime dashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == dashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<ReservationDashboardOccupancyFutureAuditDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<ReservationDashboardOccupancyFutureAuditDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<ReservationDashboardOccupancyFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbReservationDashboardOccupancyFuture> dbEntities = await _dbContext
                .OccupancyFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<ReservationDashboardOccupancyFutureAuditDto>>(dbEntities);

            return entities;
        }

        public async Task<ReservationDashboardOccupancyFutureAuditDto> InsertAsync(ReservationDashboardOccupancyFutureAuditDto dto)
        {
            var dbEntity = TinyMapper.Map<DbReservationDashboardOccupancyFutureAudit>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<ReservationDashboardOccupancyFutureAuditDto>(dbEntity);
        }

        public async Task<ReservationDashboardOccupancyFutureAuditDto> UpdateAsync(ReservationDashboardOccupancyFutureAuditDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbReservationDashboardOccupancyFutureAudit>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
