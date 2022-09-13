using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.DB.Models.Accounts;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DbContext.ApmaDbContext; 

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsLedgerBalanceRepository : IHcsLedgerBalanceRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbLedgerBalance> _ledgerBalance;

        public HcsLedgerBalanceRepository(VDVISchedulerDbContext dbContext)
        {
            _dbContext = dbContext;
            _ledgerBalance = _dbContext.LedgerBalance;
        }

        public async Task<IEnumerable<LedgerBalanceDto>> BulkInsertAsync(IEnumerable<LedgerBalanceDto> dto)
        {
            await _ledgerBalance.BulkInsertAsync(TinyMapper.Map<List<DbLedgerBalance>>(dto));
            return dto;
        }

        public async Task<string> BulkInsertWithProcAsync(IEnumerable<LedgerBalanceDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));
            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_LedgerBalance", new { ManagementSummary_LedgerBalance_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<LedgerBalanceDto> FindByIdAsync(int id)
        {
            var dbEntity = await _ledgerBalance.FindAsync(x => x.PropertyCode == "");
            return TinyMapper.Map<LedgerBalanceDto>(dbEntity);
        }

        public async Task<IEnumerable<LedgerBalanceDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbLedgerBalance> dbEntities = await _ledgerBalance.SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode).FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<LedgerBalanceDto>>(dbEntities);

            return entities;
        }

        public async Task<LedgerBalanceDto> InsertAsync(LedgerBalanceDto dto)
        {
            await _ledgerBalance.InsertAsync(TinyMapper.Map<DbLedgerBalance>(dto));
            return dto;
        }

        public async Task<LedgerBalanceDto> UpdateAsync(LedgerBalanceDto dto)
        {
            await _ledgerBalance.UpdateAsync(TinyMapper.Map<DbLedgerBalance>(dto));
            return dto;
        }

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _ledgerBalance.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _dbContext.RoomSummary.DeleteAsync(x => x.BusinessDate == businessDate);
    }
}
