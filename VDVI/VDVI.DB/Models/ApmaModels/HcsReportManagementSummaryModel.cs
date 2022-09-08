using System;
using System.Collections.Generic;
using VDVI.DB.Dtos.RoomSummary;

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

    public class LedgerBalance
    {
        public string PropertyCode { get; set; }
        public DateTime? BusinessDate { get; set; }
        public double Reservations { get; set; }
        public double InHouseReservations { get; set; }
        public double GroupReservations { get; set; }
        public double InHouseGroupReservations { get; set; }
        public double EventReservations { get; set; }
        public double TotalTurnover { get; set; }
        public double LodgingTurnover { get; set; }
        public double PaymentsDebitor { get; set; }
        public double PaymentsCash { get; set; }
        public double CityLedger { get; set; }
        public double TotalTurnoverEx { get; set; }
        public bool TotalTurnoverExSpecified { get; set; }
        public double LodgingTurnoverEx { get; set; }
        public bool LodgingTurnoverExSpecified { get; set; }


    }

    public class ManagementSummary
    {
        public DateTime? BusinessDate { get; set; }
        public RoomSummaryDto RoomSummary { get; set; }
        public LedgerBalance LedgerBalance { get; set; }
        public string PropertyCode { get; set; }
    }



    public class HcsReportManagementSummaryModel
    {
        public HcsReportManagementSummaryResult HcsReportManagementSummaryResult { get; set; }
    }
}
