using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.IRepository;
using VDVI.DB.Models.ApmaModels;
using VDVI.Services.IServices;

namespace VDVI.Services.Services
{
    public class HcsReportManagementSummaryDataInsertionService : IHcsReportManagementSummaryDataInsertionService
    {
        public IHcsReportManagementSummaryRepository _managementRoomSummaryRepository; 

        public HcsReportManagementSummaryDataInsertionService(IHcsReportManagementSummaryRepository managementRoomSummaryRepository )
        {
            _managementRoomSummaryRepository = managementRoomSummaryRepository; 
        }

        public string InsertLedgerBalance(List<LedgerBalance> ledgerBalance)
        {
           return _managementRoomSummaryRepository.InsertLedgerBalance(ledgerBalance);
        }

        public string InsertRoomSummary(List<RoomSummary> roomSummary)
        {
            return _managementRoomSummaryRepository.InsertRoomSummary(roomSummary);
        }  
    }
}
