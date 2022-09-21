﻿using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DB;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Dtos.RoomSummary;
using Dapper;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsBIOccupancyFutureRepository : DapperRepository<DbOccupancyFuture>, IHcsBIOccupancyFutureRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbOccupancyFuture> _tblRepository;

        public HcsBIOccupancyFutureRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.OccupancyFuture;
        }

        public async Task<string> BulkInsertWithProcAsync(IEnumerable<OccupancyFutureDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Occupancy_Future", new { ReservationDashboard_Occupancy_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<OccupancyFutureDto>> BulkInsertAsync(IEnumerable<OccupancyFutureDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbOccupancyFuture>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime dashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == dashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<OccupancyFutureDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<OccupancyFutureDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<OccupancyFutureDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbOccupancyFuture> dbEntities = await _dbContext
                .OccupancyFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<OccupancyFutureDto>>(dbEntities);

            return entities;
        }

        public async Task<OccupancyFutureDto> InsertAsync(OccupancyFutureDto dto)
        {
            var dbEntity = TinyMapper.Map<DbOccupancyFuture>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<OccupancyFutureDto>(dbEntity);
        }

        public async Task<OccupancyFutureDto> UpdateAsync(OccupancyFutureDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbOccupancyFuture>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
