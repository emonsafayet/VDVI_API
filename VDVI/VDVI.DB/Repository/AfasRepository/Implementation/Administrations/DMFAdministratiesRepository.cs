using CSharpFunctionalExtensions;
using Dapper;
using Framework.Core.Base.ModelEntity;
using MicroOrm.Dapper.Repositories;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using VDVI.AfasRepository.Interfaces;
using VDVI.DB.Dtos;
using VDVI.Repository.DB;
using VDVI.Repository.DbContext.AfasDbContext;
using VDVI.Repository.Models.AfasModel;
using VDVI.Repository.Models.AfasModels.Dto;

namespace VDVI.Repository.AfasRepository.Implementation
{
    public class DMFAdministratiesRepository : DapperRepository<DbDMFAdministraties>, IDMFAdministratiesRepository
    {
        private readonly AfasDbContext _dbContext;
        private readonly IDapperRepository<DbDMFAdministraties> _tblRepository;
        public DMFAdministratiesRepository(AfasDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Administraties;
        }
        public async Task<DMFAdministratiesDto> InsertAsync(DMFAdministratiesDto dto)
        {
            var dbEntity = TinyMapper.Map<DbDMFAdministraties>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<DMFAdministratiesDto>(dbEntity);
        }
        
        public async Task<DMFAdministratiesDto> UpdateAsync(DMFAdministratiesDto dto)
        {
            var entities = TinyMapper.Map<DbDMFAdministraties>(dto);

            var res = await _tblRepository.UpdateAsync(entities);

            return dto;
        }
        public async Task<IEnumerable<DMFAdministratiesDto>> BulkInsertAsync(IEnumerable<DMFAdministratiesDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbDMFAdministraties>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }
        public async Task<string> BulkInsertWithProcAsync(IEnumerable<DMFAdministratiesDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>(" spINSERT_dmf_Administraties", new { RatePlanStatistics_History_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }
        public async Task<DMFAdministratiesDto> FindByCodeAsync(string code)
        {
            var dbEntity = await _tblRepository.FindAsync(x => x.Administratietype_code == code);

            var dto = TinyMapper.Map<DMFAdministratiesDto>(dbEntity);

            return dto;
        } 
        
    }
}
