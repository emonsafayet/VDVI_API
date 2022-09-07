using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.Services.Interfaces
{
    public interface IHcsReportManagementSummaryService
    {
        void GetManagementSummary();
        void ManagementSummaryInsertRoomAndLedger();
        string ManagementSummaryInsertManullyRoomAndLedger(string _startDate, string _endDate);

    }
}
