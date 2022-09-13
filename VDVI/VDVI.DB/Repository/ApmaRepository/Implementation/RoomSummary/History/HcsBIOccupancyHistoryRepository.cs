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
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Dtos.RoomSummary; 

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIOccupancyHistoryRepository : DapperRepository<DbOccupancyHistory>, IHcsBIOccupancyHistoryRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbOccupancyHistory> _tblRepository;

        public HcsBIOccupancyHistoryRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Occupancy;
        }

        public async Task<string> BulkInsertWithProcAsync(IEnumerable<OccupancyHistoryDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Occupancy_History", new { ReservationDashboard_Occupancy_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<OccupancyHistoryDto>> BulkInsertAsync(IEnumerable<OccupancyHistoryDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbOccupancyHistory>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime dashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == dashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<OccupancyHistoryDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<OccupancyHistoryDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<OccupancyHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbOccupancyHistory> dbEntities = await _dbContext
                .Occupancy
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<OccupancyHistoryDto>>(dbEntities);

            return entities;
        }

        public async Task<OccupancyHistoryDto> InsertAsync(OccupancyHistoryDto dto)
        {
            var dbEntity = TinyMapper.Map<DbOccupancyHistory>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<OccupancyHistoryDto>(dbEntity);
        }

        public async Task<OccupancyHistoryDto> UpdateAsync(OccupancyHistoryDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbOccupancyHistory>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
