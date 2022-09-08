using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.RoomSummary
{

    public class OccupancyDto
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public decimal Percentage { get; set; }
        public int RoomsSold { get; set; }
        public int AvailableRooms { get; set; }
    }
}
