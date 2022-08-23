using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.Services.IServices
{
    public interface IReportManagementDataInsertionService
    {
        void InsertRoomSummary(List<RoomSummary> roomSummary);
        void InsertLedgerBalance(List<LedgerBalance> ledgerBalance);
    }
}
