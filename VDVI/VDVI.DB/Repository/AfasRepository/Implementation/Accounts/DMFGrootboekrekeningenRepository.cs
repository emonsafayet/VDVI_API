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
    
    public class DMFGrootboekrekeningenRepository : DapperRepository<DbDMFGrootboekrekeningen>, IDMFGrootboekrekeningenRepository
    {
        private readonly AfasDbContext _dbContext;
        private readonly IDapperRepository<DbDMFGrootboekrekeningen> _tblRepository;
        public DMFGrootboekrekeningenRepository(AfasDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.Grootboekrekeningen;
        }
        public async Task<DMFGrootboekrekeningenDto> InsertAsync(DMFGrootboekrekeningenDto dto)
        {
            var dbEntity = TinyMapper.Map<DbDMFGrootboekrekeningen>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<DMFGrootboekrekeningenDto>(dbEntity);
        }
        public async Task<DMFGrootboekrekeningenDto> UpdateAsync(DMFGrootboekrekeningenDto dto)
        {
            var entities = TinyMapper.Map<DbDMFGrootboekrekeningen>(dto);

            var res = await _tblRepository.UpdateAsync(entities);

            return dto;
        }
        public async Task<IEnumerable<DMFGrootboekrekeningenDto>> BulkInsertAsync(IEnumerable<DMFGrootboekrekeningenDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbDMFGrootboekrekeningen>>(dto);

            await _tblRepository.BulkInsertAsync(dbEntity);

            return dto;
        }
        public async Task<string> BulkInsertWithProcAsync(IEnumerable<DMFGrootboekrekeningenDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));

            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_dmf_Grootboekrekeningen", new { Grootboekrekeningen_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        } 
    }
}
