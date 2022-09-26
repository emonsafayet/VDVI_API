using System;
using VDVI.Repository.Models.Common;

namespace VDVI.DB.Dtos
{
    public class OccupancyHistoryDto : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public decimal Percentage { get; set; }
        public int RoomsSold { get; set; }
        public int AvailableRooms { get; set; }
    }
}
