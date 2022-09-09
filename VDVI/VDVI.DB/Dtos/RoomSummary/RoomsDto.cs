using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.RoomSummary
{
    public class RoomsDto
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public int Inventory { get; set; }
        public int OutOfInventory { get; set; }
        public int OutOfOrder { get; set; }
    }
}
