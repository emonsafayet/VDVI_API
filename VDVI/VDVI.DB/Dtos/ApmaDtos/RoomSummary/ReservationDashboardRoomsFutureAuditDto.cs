using System;
using VDVI.Repository.Models.Common;

namespace VDVI.DB.Dtos
{
    public class ReservationDashboardRoomsFutureAuditDto : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public int Inventory { get; set; }
        public int OutOfInventory { get; set; }
        public int OutOfOrder { get; set; }
    }
}
