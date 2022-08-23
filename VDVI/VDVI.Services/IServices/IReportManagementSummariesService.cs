using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.IServices
{
    public interface IReportManagementSummariesService
    { 
        void GetManagementData();
        void InsertReportManagenetRoomAndLedgerData();

    }
}
