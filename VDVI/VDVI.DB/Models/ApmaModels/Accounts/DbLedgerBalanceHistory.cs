using System;
using System.ComponentModel.DataAnnotations.Schema;
using VDVI.Repository.Models;

namespace VDVI.Repository.DB
{
    [Table("[hce].[ManagementSummary_LedgerBalance_History]")]
    public class DbLedgerBalanceHistory: Audit
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
    }
}
