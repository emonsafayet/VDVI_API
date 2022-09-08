using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.RoomSummary
{
    public class RoomsDto
    {
        public int Inventory { get; set; }
        public int OutOfInventory { get; set; }
        public int OutOfOrder { get; set; }
    }
}
