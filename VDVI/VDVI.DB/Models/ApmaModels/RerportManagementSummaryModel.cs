using System;
using System.Collections.Generic;
using System.Text;

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
        public DateTime? BusinessDate { get; set; }
        public string PropertyCode { get; set; }
    }

    public class ManagementSummary
    {
        public DateTime? BusinessDate { get; set; }
        public RoomSummary RoomSummary { get; set; }
        public LedgerBalance LedgerBalance { get; set; }
        public string PropertyCode { get; set; }
    }

    public class RoomSummary
    {
        public int InHouse { get; set; }
        public int DayUse { get; set; }
        public int LateArrival { get; set; }
        public int EarlyDeparture { get; set; }
        public int Departed { get; set; }
        public int ToDepart { get; set; }
        public int StayOver { get; set; }
        public int EarlyArrival { get; set; }
        public int Arrived { get; set; }
        public int ToArrive { get; set; }
        public int NoShow { get; set; }
        public int Complementary { get; set; }
        public int WalkIns { get; set; }
        public int RoomReservationCreated { get; set; }
        public int RoomReservationCancelled { get; set; }
        public DateTime? BusinessDate { get; set; }
        public string PropertyCode { get; set; }
    }

    public class RerportManagementSummaryModel
    {
        public HcsReportManagementSummaryResult HcsReportManagementSummaryResult { get; set; }
    }
}
