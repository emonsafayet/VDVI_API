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
using VDVI.Repository.Dtos.RoomSummary;
using VDVI.Repository.Interfaces;

namespace VDVI.Repository.Implementation
{
    public class HcsBIOccupancyRepository : DapperRepository<DbOccupancy>, IHcsBIOccupancyRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbOccupancy> _tblRepository;

        public HcsBIOccupancyRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Occupancy;
        }

        public async Task<string> BulkInsertWithProcAsync(IEnumerable<OccupancyDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Occupancy_History", new { ReservationDashboard_Occupancy_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<OccupancyDto>> BulkInsertAsync(IEnumerable<OccupancyDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbOccupancy>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime dashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == dashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<OccupancyDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<OccupancyDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<OccupancyDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbOccupancy> dbEntities = await _dbContext
                .Occupancy
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<OccupancyDto>>(dbEntities);

            return entities;
        }

        public async Task<OccupancyDto> InsertAsync(OccupancyDto dto)
        {
            var dbEntity = TinyMapper.Map<DbOccupancy>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<OccupancyDto>(dbEntity);
        }

        public async Task<OccupancyDto> UpdateAsync(OccupancyDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbOccupancy>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
