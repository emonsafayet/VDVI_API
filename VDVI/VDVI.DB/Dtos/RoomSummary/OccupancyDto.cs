using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.RoomSummary
{

    public class OccupancyDto
    {
        public double Percentage { get; set; }
        public int RoomsSold { get; set; }
        public int AvailableRooms { get; set; }
    }
}
