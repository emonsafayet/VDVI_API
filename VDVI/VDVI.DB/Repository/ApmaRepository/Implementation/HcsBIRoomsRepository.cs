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
    public class HcsBIRoomsRepository: DapperRepository<DbRooms>, IHcsBIRoomsRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRooms> _tblRepository;

        public HcsBIRoomsRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Rooms;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RoomsDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Rooms_History", new { ReservationDashboard_Rooms_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RoomsDto>> BulkInsertAsync(IEnumerable<RoomsDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRooms>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RoomsDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RoomsDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RoomsDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRooms> dbEntities = await _dbContext
                .Rooms
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RoomsDto>>(dbEntities);

            return entities;
        }

        public async Task<RoomsDto> InsertAsync(RoomsDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRooms>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RoomsDto>(dbEntity);
        }

        public async Task<RoomsDto> UpdateAsync(RoomsDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRooms>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
