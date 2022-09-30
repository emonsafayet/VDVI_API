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
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.DB;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class HcsLedgerBalanceHistoryRepository : IHcsLedgerBalanceHistoryRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbLedgerBalanceHistory> _ledgerBalance;

        public HcsLedgerBalanceHistoryRepository(VDVISchedulerDbContext dbContext)
        {
            _dbContext = dbContext;
            _ledgerBalance = _dbContext.LedgerBalance;
        }

        public async Task<IEnumerable<LedgerBalanceHistoryDto>> BulkInsertAsync(IEnumerable<LedgerBalanceHistoryDto> dto)
        {
            await _ledgerBalance.BulkInsertAsync(TinyMapper.Map<List<DbLedgerBalanceHistory>>(dto));
            return dto;
        }

        public async Task<string> BulkInsertWithProcAsync(IEnumerable<LedgerBalanceHistoryDto> dto)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dto));
            var queryResult = await _dbContext.Connection.QueryAsync<string>("spINSERT_hce_ManagementSummary_LedgerBalance_History", new { ManagementSummary_LedgerBalance_UDT = dt }, commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }

        public async Task<LedgerBalanceHistoryDto> FindByIdAsync(int id)
        {
            var dbEntity = await _ledgerBalance.FindAsync(x => x.PropertyCode == "");
            return TinyMapper.Map<LedgerBalanceHistoryDto>(dbEntity);
        }

        public async Task<IEnumerable<LedgerBalanceHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbLedgerBalanceHistory> dbEntities = await _ledgerBalance.SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode).FindAllAsync(x => x.PropertyCode == propertyCode);

            var entities = TinyMapper.Map<List<LedgerBalanceHistoryDto>>(dbEntities);

            return entities;
        }

        public async Task<LedgerBalanceHistoryDto> InsertAsync(LedgerBalanceHistoryDto dto)
        {
            await _ledgerBalance.InsertAsync(TinyMapper.Map<DbLedgerBalanceHistory>(dto));
            return dto;
        }

        public async Task<LedgerBalanceHistoryDto> UpdateAsync(LedgerBalanceHistoryDto dto)
        {
            await _ledgerBalance.UpdateAsync(TinyMapper.Map<DbLedgerBalanceHistory>(dto));
            return dto;
        }

        public async Task<bool> DeleteByPropertyCodeAsync(string propertyCode) => await _ledgerBalance.DeleteAsync(x => x.PropertyCode == propertyCode);

        public async Task<bool> DeleteByBusinessDateAsync(DateTime businessDate) => await _dbContext.RoomSummary.DeleteAsync(x => x.BusinessDate == businessDate);
    }
}
