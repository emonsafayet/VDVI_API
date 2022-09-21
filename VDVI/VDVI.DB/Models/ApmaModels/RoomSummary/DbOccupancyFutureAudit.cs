using System;
using System.ComponentModel.DataAnnotations.Schema;
using VDVI.Repository.Models.Common;

namespace VDVI.Repository.DB
{
    [Table("[hce].[ReservationDashboardOccupancyFutureAudit]")]
    internal class DbOccupancyFutureAudit : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public decimal Percentage { get; set; }
        public int RoomsSold { get; set; }
        public int AvailableRooms { get; set; }
    }
}
