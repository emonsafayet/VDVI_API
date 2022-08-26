using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.IServices
{
    public interface IReportManagementSummariesService
    { 
        void GetManagementData();
        void InsertReportManagenetRoomAndLedgerSummary();
        string InsertManullyReportManagementRoomAndLedgerSummary(string _startDate, string _endDate);

    }
}
