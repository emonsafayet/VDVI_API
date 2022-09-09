using System;
using System.Collections.Generic;
using VDVI.DB.Dtos;

namespace VDVI.DB.Models.ApmaModels
{
    public class ErrorInfo
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorData { get; set; }
    }

    public class HcsReportManagementSummaryResult
    {
        public string PropertyCode { get; set; }
        public List<ManagementSummary> ManagementSummaries { get; set; }
        public string UniqueID { get; set; }
        public bool Success { get; set; }
        public ErrorInfo ErrorInfo { get; set; }
        public object WarningInfo { get; set; }
    }

    public class ManagementSummary
    {
        public DateTime? BusinessDate { get; set; }
        public RoomSummaryDto RoomSummary { get; set; }
        public LedgerBalanceDto LedgerBalance { get; set; }
        public string PropertyCode { get; set; }
    }



    public class HcsReportManagementSummaryModel
    {
        public HcsReportManagementSummaryResult HcsReportManagementSummaryResult { get; set; }
    }
}
