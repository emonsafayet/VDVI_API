using System;
using VDVI.Repository.Models;

namespace VDVI.DB.Dtos
{
    public class LedgerBalanceHistoryDto : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? BusinessDate { get; set; }
        public decimal Reservations { get; set; }
        public decimal InHouseReservations { get; set; }
        public decimal GroupReservations { get; set; }
        public decimal InHouseGroupReservations { get; set; }
        public decimal EventReservations { get; set; }
        public decimal TotalTurnover { get; set; }
        public decimal LodgingTurnover { get; set; }
        public decimal PaymentsDebitor { get; set; }
        public decimal PaymentsCash { get; set; }
        public decimal CityLedger { get; set; } 
    }
}
