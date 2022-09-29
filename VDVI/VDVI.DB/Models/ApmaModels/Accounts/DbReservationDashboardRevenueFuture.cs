using System;
using System.ComponentModel.DataAnnotations.Schema;
using VDVI.Repository.Models.Common;

namespace VDVI.Repository.DB
{
    [Table("[hce].[ReservationDashboardRevenueFuture]")]
    public class DbReservationDashboardRevenueFuture : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public decimal TypeA { get; set; }
        public decimal TypeB { get; set; }
        public decimal TypeC { get; set; }
        public decimal TypeD { get; set; }
        public decimal TypeE { get; set; }
        public decimal TypeF { get; set; }
        public decimal Undefined { get; set; }
        public decimal AverageDailyRate { get; set; }
        public decimal RevenuePerAvailableRoom { get; set; }
        public decimal SpentPerAvailableRoom { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
