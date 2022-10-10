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
using VDVI.Repository.AfasDtos;
using VDVI.Repository.AfasModels;
using VDVI.Repository.DB;
using VDVI.Repository.DbContext.AfasDbContext; 
using VDVI.Repository.Models.AfasModels;
using VDVI.Repository.Models.AfasModels.Dto;

namespace VDVI.Repository.AfasRepository.Implementation
{
    
    public class DMFBeginbalansRepository : DapperRepository<DbDMFBeginbalans>, IDMFBeginbalansRepository
    {
        private readonly AfasDbContext _dbContext;
        private readonly IDapperRepository<DbDMFBeginbalans> _tblRepository;
        public DMFBeginbalansRepository(AfasDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Beginbalans;
        }
        public async Task<DMFBeginbalansDto> InsertAsync(DMFBeginbalansDto dto)
        {
            var dbEntity = TinyMapper.Map<DbDMFBeginbalans>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<DMFBeginbalansDto>(dbEntity);
        }
        
        public async Task<DMFBeginbalansDto> UpdateAsync(DMFBeginbalansDto dto)
        {
            var entities = TinyMapper.Map<DbDMFBeginbalans>(dto);

            var res = await _tblRepository.UpdateAsync(entities);

            return dto;
        }
        public async Task<IEnumerable<DMFBeginbalansDto>> BulkInsertAsync(IEnumerable<DMFBeginbalansDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbDMFBeginbalans>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }
        public async Task<string> BulkInsertWithProcAsync(IEnumerable<DMFBeginbalansDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_dmf_Beginbalans", new { Beginbalans_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        } 
    }
}
