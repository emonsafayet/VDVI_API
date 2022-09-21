﻿using Dapper;
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
    public class HcsBIRoomsFutureAuditRepository: DapperRepository<DbRoomsFutureAudit>, IHcsBIRoomsFutureAuditRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbRoomsFutureAudit> _tblRepository;

        public HcsBIRoomsFutureAuditRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.RoomsFutureAudit;
        }


        public async Task<string> BulkInsertWithProcAsync(IEnumerable<RoomsFutureAuditDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ReservationDashboard_Rooms_Future_Audit", new { ReservationDashboard_Rooms_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<IEnumerable<RoomsFutureAuditDto>> BulkInsertAsync(IEnumerable<RoomsFutureAuditDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRoomsFutureAudit>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate) => await _tblRepository.DeleteAsync(x => x.DashboardDate == DashboardDate);

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _tblRepository.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<RoomsFutureAuditDto> FindByIdAsync(int id)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RoomsFutureAuditDto>(dbEntity);

            return dto;
        }

        public async Task<IEnumerable<RoomsFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRoomsFuture> dbEntities = await _dbContext
                .RoomsFuture
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<RoomsFutureAuditDto>>(dbEntities);

            return entities;
        }

        public async Task<RoomsFutureAuditDto> InsertAsync(RoomsFutureAuditDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRoomsFutureAudit>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<RoomsFutureAuditDto>(dbEntity);
        }

        public async Task<RoomsFutureAuditDto> UpdateAsync(RoomsFutureAuditDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRoomsFutureAudit>(dto);

            await _tblRepository.UpdateAsync(dbCustomerEntity);

            return dto;
        }
    }
}
