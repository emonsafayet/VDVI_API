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
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Interfaces;

namespace VDVI.Repository.Implementation
{
    public class HcsBIRevenueRepository: DapperRepository<DbRevenue>, IHcsBIRevenueRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRevenue> _tblRepository;

        public HcsBIRevenueRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Revenue;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RevenueDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Revenue_History", new { RatePlanStatistics_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RevenueDto>> BulkInsertAsync(IEnumerable<RevenueDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRevenue>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RevenueDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RevenueDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RevenueDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRevenue> dbEntities = await _dbContext
                .Revenue
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RevenueDto>>(dbEntities);

            return entities;
        }

        public async Task<RevenueDto> InsertAsync(RevenueDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRevenue>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RevenueDto>(dbEntity);
        }

        public async Task<RevenueDto> UpdateAsync(RevenueDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRevenue>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
