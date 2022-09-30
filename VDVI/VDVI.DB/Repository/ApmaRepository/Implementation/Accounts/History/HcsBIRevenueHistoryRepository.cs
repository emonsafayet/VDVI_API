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
    public class HcsBIRevenueHistoryRepository : DapperRepository<DbReservationDashboardRevenueHistory>, IHcsBIRevenueHistoryRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbReservationDashboardRevenueHistory> _tblRepository;

        public HcsBIRevenueHistoryRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Revenue;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDashboardRevenueHistoryDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Revenue_History", new { ReservationDashboard_Revenue_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<ReservationDashboardRevenueHistoryDto>> BulkInsertAsync(IEnumerable<ReservationDashboardRevenueHistoryDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbReservationDashboardRevenueHistory>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<ReservationDashboardRevenueHistoryDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<ReservationDashboardRevenueHistoryDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<ReservationDashboardRevenueHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbReservationDashboardRevenueHistory> dbEntities = await _dbContext
                .Revenue
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<ReservationDashboardRevenueHistoryDto>>(dbEntities);

            return entities;
        }

        public async Task<ReservationDashboardRevenueHistoryDto> InsertAsync(ReservationDashboardRevenueHistoryDto dto)
        {
            var dbEntity = TinyMapper.Map<DbReservationDashboardRevenueHistory>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<ReservationDashboardRevenueHistoryDto>(dbEntity);
        }

        public async Task<ReservationDashboardRevenueHistoryDto> UpdateAsync(ReservationDashboardRevenueHistoryDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbReservationDashboardRevenueHistory>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
