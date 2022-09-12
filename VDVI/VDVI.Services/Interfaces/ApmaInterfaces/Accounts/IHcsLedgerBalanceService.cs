﻿using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.Services.Interfaces.Apma.Accounts
{
    public interface IHcsLedgerBalanceService
    {
        Task<Result<PrometheusResponse>> InsertAsync(LedgerBalanceDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<LedgerBalanceDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<LedgerBalanceDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}