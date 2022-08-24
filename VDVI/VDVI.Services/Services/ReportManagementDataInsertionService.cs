using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.IRepository;
using VDVI.DB.Models.ApmaModels;
using VDVI.Services.IServices;

namespace VDVI.Services.Services
{
    public class ReportManagementDataInsertionService : IReportManagementDataInsertionService
    {
        public IManagementRoomSummaryRepository _managementRoomSummaryRepository; 

        public ReportManagementDataInsertionService(IManagementRoomSummaryRepository managementRoomSummaryRepository )
        {
            _managementRoomSummaryRepository = managementRoomSummaryRepository; 
        }

        public void InsertLedgerBalance(List<LedgerBalance> ledgerBalance)
        {
            _managementRoomSummaryRepository.InsertLedgerBalance(ledgerBalance);
        }

        public void InsertRoomSummary(List<RoomSummary> roomSummary)
        {
            _managementRoomSummaryRepository.InsertRoomSummary(roomSummary);
        } 
    }
}
