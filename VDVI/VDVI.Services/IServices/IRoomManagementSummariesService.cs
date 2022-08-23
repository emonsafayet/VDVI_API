using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.IServices
{
    public interface IRoomManagementSummariesService
    { 
        void InsertRoomSummary(RoomSummary roomSummary);
        void InsertLedgerBalance(LedgerBalance ledgerBalance);

        

    }
}
