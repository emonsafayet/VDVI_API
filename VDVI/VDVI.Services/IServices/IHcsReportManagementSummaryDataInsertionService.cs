using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.Services.IServices
{
    public interface IHcsReportManagementSummaryDataInsertionService
    {
        string InsertRoomSummary(List<RoomSummary> roomSummary);
        string InsertLedgerBalance(List<LedgerBalance> ledgerBalance);
    }
}
