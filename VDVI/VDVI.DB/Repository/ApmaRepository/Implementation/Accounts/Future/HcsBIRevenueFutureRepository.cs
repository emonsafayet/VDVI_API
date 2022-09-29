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
    public class HcsBIRevenueFutureRepository : DapperRepository<DbReservationDashboardRevenueFuture>, IHcsBIRevenueFutureRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbReservationDashboardRevenueFuture> _tblRepository;

        public HcsBIRevenueFutureRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RevenueFuture;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDashboardRevenueFutureDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Revenue_Future", new { ReservationDashboardRevenueFutureUDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<ReservationDashboardRevenueFutureDto>> BulkInsertAsync(IEnumerable<ReservationDashboardRevenueFutureDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbReservationDashboardRevenueFuture>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<ReservationDashboardRevenueFutureDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<ReservationDashboardRevenueFutureDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<ReservationDashboardRevenueFutureDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbReservationDashboardRevenueFuture> dbEntities = await _dbContext
                .RevenueFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<ReservationDashboardRevenueFutureDto>>(dbEntities);

            return entities;
        }

        public async Task<ReservationDashboardRevenueFutureDto> InsertAsync(ReservationDashboardRevenueFutureDto dto)
        {
            var dbEntity = TinyMapper.Map<DbReservationDashboardRevenueFuture>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<ReservationDashboardRevenueFutureDto>(dbEntity);
        }

        public async Task<ReservationDashboardRevenueFutureDto> UpdateAsync(ReservationDashboardRevenueFutureDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbReservationDashboardRevenueFuture>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
