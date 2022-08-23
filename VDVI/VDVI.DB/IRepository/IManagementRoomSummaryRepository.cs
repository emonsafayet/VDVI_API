using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.IRepository
{
    public  interface IManagementRoomSummaryRepository
    {
        void InsertRoomSummary(RoomSummary roomSummary);
        void InsertLedgerBalance(LedgerBalance ledgerBalance);
    }
}
