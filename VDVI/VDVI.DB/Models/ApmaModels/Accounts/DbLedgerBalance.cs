using System;
using System.ComponentModel.DataAnnotations.Schema;
using VDVI.Repository.Models.Common;

namespace VDVI.DB.Models.Accounts
{
    [Table("[hce].[ManagementSummary_LedgerBalance]")]
    public class DbLedgerBalance: Audit
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
}
