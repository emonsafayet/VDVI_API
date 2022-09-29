using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Models.Common;

namespace VDVI.DB.Dtos
{
    public class RoomsHistoryDto : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public int Inventory { get; set; }
        public int OutOfInventory { get; set; }
        public int OutOfOrder { get; set; }
    }
}
