﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using VDVI.Repository.Models.Common;

namespace VDVI.Repository.DB
{
    [Table("[hce].[ReservationDashboardRoomsFutureAudit]")]
    public class DbRoomsFutureAudit : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? DashboardDate { get; set; }
        public int Inventory { get; set; }
        public int OutOfInventory { get; set; }
        public int OutOfOrder { get; set; }
    }
}