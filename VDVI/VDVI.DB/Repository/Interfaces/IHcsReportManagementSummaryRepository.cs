using System.Collections.Generic;
using VDVI.DB.Dtos;

namespace VDVI.DB.IRepository
{
    public interface IHcsReportManagementSummaryRepository
    {
        string InsertRoomSummary(List<RoomSummaryDto> roomSummary);
        string InsertLedgerBalance(List<LedgerBalanceDto> ledgerBalance);
    }
}
