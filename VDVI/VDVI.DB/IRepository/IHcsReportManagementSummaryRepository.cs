using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.IRepository
{
    public  interface IHcsReportManagementSummaryRepository
    {
        string InsertRoomSummary(List<RoomSummary> roomSummary);
        string InsertLedgerBalance(List<LedgerBalance> ledgerBalance);
    }
}
