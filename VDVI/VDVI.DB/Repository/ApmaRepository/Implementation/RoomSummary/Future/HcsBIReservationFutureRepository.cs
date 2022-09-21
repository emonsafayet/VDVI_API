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
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DB;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIReservationFutureRepository : DapperRepository<DbReservationFuture>, IHcsBIReservationFutureRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbReservationFuture> _tblRepository;

        public HcsBIReservationFutureRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.ReservationFuture;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationFutureDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Reservation_History",
                            new { ReservationDashboard_Reservation_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<ReservationFutureDto>> BulkInsertAsync(IEnumerable<ReservationFutureDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbReservationFuture>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime businessDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == businessDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<ReservationFutureDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<ReservationFutureDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<ReservationFutureDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbReservationFuture> dbEntities = await _dbContext
                .ReservationFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<ReservationFutureDto>>(dbEntities);

            return entities;
        }

        public async Task<ReservationFutureDto> InsertAsync(ReservationFutureDto dto)
        {
            var dbEntity = TinyMapper.Map<DbReservationFuture>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<ReservationFutureDto>(dbEntity);
        }

        public async Task<ReservationFutureDto> UpdateAsync(ReservationFutureDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbReservationFuture>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
