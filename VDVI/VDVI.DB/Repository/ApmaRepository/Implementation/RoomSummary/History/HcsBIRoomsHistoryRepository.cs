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
using VDVI.ApmaRepository.Interfaces;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIRoomsHistoryRepository : DapperRepository<DbRoomsHistory>, IHcsBIRoomsHistoryRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRoomsHistory> _tblRepository;

        public HcsBIRoomsHistoryRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Rooms;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RoomsHistoryDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Rooms_History", new { ReservationDashboard_Rooms_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RoomsHistoryDto>> BulkInsertAsync(IEnumerable<RoomsHistoryDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRoomsHistory>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RoomsHistoryDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RoomsHistoryDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RoomsHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRoomsHistory> dbEntities = await _dbContext
                .Rooms
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RoomsHistoryDto>>(dbEntities);

            return entities;
        }

        public async Task<RoomsHistoryDto> InsertAsync(RoomsHistoryDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRoomsHistory>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RoomsHistoryDto>(dbEntity);
        }

        public async Task<RoomsHistoryDto> UpdateAsync(RoomsHistoryDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRoomsHistory>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
