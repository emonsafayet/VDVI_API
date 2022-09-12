using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.RoomSummary;
using VDVI.Repository.Interfaces;

namespace VDVI.Repository.Implementation
{
    public class HcsBIReservationRepository: DapperRepository<DbReservation>, IHcsBIReservationRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbReservation> _tblRepository;

        public HcsBIReservationRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Reservation;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Reservation_History", new { ReservationDashboard_Reservation_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<ReservationDto>> BulkInsertAsync(IEnumerable<ReservationDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbReservation>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime businessDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == businessDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<ReservationDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<ReservationDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbReservation> dbEntities = await _dbContext
                .Reservation
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<ReservationDto>>(dbEntities);

            return entities;
        }

        public async Task<ReservationDto> InsertAsync(ReservationDto dto)
        {
            var dbEntity = TinyMapper.Map<DbReservation>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<ReservationDto>(dbEntity);
        }

        public async Task<ReservationDto> UpdateAsync(ReservationDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbReservation>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
