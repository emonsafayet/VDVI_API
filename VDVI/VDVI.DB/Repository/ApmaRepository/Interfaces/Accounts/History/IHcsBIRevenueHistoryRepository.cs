﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRevenueHistoryRepository
    {
        Task<RevenueHistoryDto> InsertAsync(RevenueHistoryDto dto);
        Task<IEnumerable<RevenueHistoryDto>> BulkInsertAsync(IEnumerable<RevenueHistoryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RevenueHistoryDto> dto);
        Task<RevenueHistoryDto> UpdateAsync(RevenueHistoryDto dto);
        Task<IEnumerable<RevenueHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RevenueHistoryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
