using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using VDVI.DB.IRepository;
using VDVI.DB.IServices;
using VDVI.DB.Models.ApmaModels; 

namespace VDVI.DB.Services
{
    public class RoomManagementSummariesService : IRoomManagementSummariesService
    {
        public IManagementRoomSummaryRepository _managementRoomSummaryRepository;

        public RoomManagementSummariesService(IManagementRoomSummaryRepository managementRoomSummaryRepository)
        {
            _managementRoomSummaryRepository = managementRoomSummaryRepository;
        }
       
        public void InsertLedgerBalance(LedgerBalance ledgerBalance)
        {
            //business logic
            _managementRoomSummaryRepository.InsertLedgerBalance(ledgerBalance);
        }

        public void InsertRoomSummary(RoomSummary roomSummary)
        {
            //business logic
            _managementRoomSummaryRepository.InsertRoomSummary(roomSummary);
        }
    }
}
