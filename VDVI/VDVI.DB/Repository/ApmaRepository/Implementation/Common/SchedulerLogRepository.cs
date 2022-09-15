using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VDVI.DB.Models.Common;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.SourceStatistics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VDVI.Repository.DB;
using VDVI.Repository.Dtos.ApmaDtos.Common;
using VDVI.DB.Dtos;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class SchedulerLogRepository : DapperRepository<DbSchedulerLog>, ISchedulerLogRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbSchedulerLog> _tblRepository;

        public SchedulerLogRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.SchedulerLog;
        }
        public async Task<SchedulerLogDto> InsertAsync(SchedulerLogDto dto)
        {
            var dbEntity = TinyMapper.Map<DbSchedulerLog>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<SchedulerLogDto>(dbEntity);
        }
        public Task<IEnumerable<SchedulerLogDto>> BulkInsertAsync(IEnumerable<SchedulerLogDto> dto)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteByMethodNameAsync(string methodName) => await _tblRepository.DeleteAsync(x => x.MethodName == methodName);

        public async Task<bool> DeleteByDateAsync(DateTime executionDate) => await _tblRepository.DeleteAsync(x => x.ExecutionDateTime == executionDate);
        public async Task<SchedulerLogDto> FindByMethodNameAsync(string methodName)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.MethodName == methodName);

            var dto = TinyMapper.Map<SchedulerLogDto>(dbEntity);

            return dto;
        }
        public async Task<IEnumerable<SchedulerLogDto>> GetAllByMethodNameAsync(string methodName)
        {
            IEnumerable<DbSchedulerLog> dbEntities = await _dbContext
                .SchedulerLog
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.MethodName)
                .FindAllAsync(x => x.MethodName==methodName);

            var entities = TinyMapper.Map<List<SchedulerLogDto>>(dbEntities);

            return entities;
        }
        public async Task<SchedulerLogDto> UpdateAsync(SchedulerLogDto dto)
        {
            var entity = TinyMapper.Map<DbSchedulerLog>(dto);

            await _tblRepository.UpdateAsync(entity);

            return dto;
        }

        
    }
}
