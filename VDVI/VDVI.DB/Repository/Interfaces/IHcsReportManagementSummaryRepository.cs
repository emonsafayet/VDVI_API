using System.Collections.Generic;
using VDVI.DB.Dtos.Accounts;
using VDVI.DB.Dtos.RoomSummary;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.IRepository
{
    public interface IHcsReportManagementSummaryRepository
    {
        string InsertRoomSummary(List<RoomSummaryDto> roomSummary);
        string InsertLedgerBalance(List<LedgerBalanceDto> ledgerBalance);
    }
}
