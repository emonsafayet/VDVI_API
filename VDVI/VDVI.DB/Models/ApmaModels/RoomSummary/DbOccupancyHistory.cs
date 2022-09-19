using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDVI.Repository.Models.Common;

namespace VDVI.Repository.DB
{
    [Table("[hce].[ReservationDashboardOccupancyHistory]")]
    public class DbOccupancyHistory : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public decimal Percentage { get; set; }
        public int RoomsSold { get; set; }
        public int AvailableRooms { get; set; }
    }
}
