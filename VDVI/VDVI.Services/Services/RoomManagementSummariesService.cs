using Newtonsoft.Json;
using SOAPAppCore.Interfaces.Apma;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VDVI.DB.IRepository;
using VDVI.DB.IServices;
using VDVI.DB.Models.ApmaModels; 

namespace VDVI.DB.Services
{
    public class RoomManagementSummariesService : IRoomManagementSummariesService
    {

        public IManagementRoomSummaryRepository _managementRoomSummaryRepository;
        private readonly IReportManagementSummary _reportSummary;

        public RoomManagementSummariesService(IManagementRoomSummaryRepository managementRoomSummaryRepository, IReportManagementSummary reportSummary)
        {
            _managementRoomSummaryRepository = managementRoomSummaryRepository;
            _reportSummary = reportSummary;
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

        public void  Calling() 
        
        {

            //InsertLedgerBalance();


        }






        public void GetManagementData() {
            DateTime startDate = new DateTime();
            DateTime enddate = new DateTime();
            List<HcsReportManagementSummaryResponse> res = _reportSummary.GetReportManagementSummaryFromApma(startDate, enddate);

            //TODO: Go to service for data binding 

            var jsonDatas = JsonConvert.SerializeObject(res, formatting: Newtonsoft.Json.Formatting.Indented);

            List<RerportManagementSummaryModel> reportManagementSummaries = JsonConvert.DeserializeObject<List<RerportManagementSummaryModel>>(jsonDatas);


            List<HcsReportManagementSummaryResult> filterreportManagementSummaries = new List<HcsReportManagementSummaryResult>();
            filterreportManagementSummaries = reportManagementSummaries.Where(r => r.HcsReportManagementSummaryResult.Success == true).ToList().Select(x => x.HcsReportManagementSummaryResult).ToList();

            List<VDVI.DB.Models.ApmaModels.ManagementSummary> managementSummaryList = new List<DB.Models.ApmaModels.ManagementSummary>();
            managementSummaryList = GetmanagementSummaryList(filterreportManagementSummaries);

            List<DB.Models.ApmaModels.RoomSummary> roomSummaryList = GetRoomSummary(managementSummaryList);

            List<DB.Models.ApmaModels.LedgerBalance> ledgerBalanceList = GetLedgerSummary(managementSummaryList);

        }



        private List<VDVI.DB.Models.ApmaModels.ManagementSummary> GetmanagementSummaryList(List<VDVI.DB.Models.ApmaModels.HcsReportManagementSummaryResult> filterreportManagementSummaries)
        {
            var list = filterreportManagementSummaries.Select(a => new DB.Models.ApmaModels.ManagementSummary()
            {
                PropertyCode = a.PropertyCode,
                BusinessDate = a.ManagementSummaries.Select(x => x.BusinessDate).FirstOrDefault(),
                RoomSummary = a.ManagementSummaries.Select(x => x.RoomSummary).FirstOrDefault(),
                LedgerBalance = a.ManagementSummaries.Select(x => x.LedgerBalance).FirstOrDefault(),

            }).ToList();
            return list;
        }

        private List<DB.Models.ApmaModels.RoomSummary> GetRoomSummary(List<VDVI.DB.Models.ApmaModels.ManagementSummary> managementSummaryList)
        {

            List<DB.Models.ApmaModels.RoomSummary> roomSummaries = managementSummaryList.Select(x => new DB.Models.ApmaModels.RoomSummary()
            {
                InHouse = x.RoomSummary.InHouse,
                DayUse = x.RoomSummary.DayUse,
                LateArrival = x.RoomSummary.LateArrival,
                EarlyDeparture = x.RoomSummary.EarlyDeparture,
                Departed = x.RoomSummary.Departed,
                ToDepart = x.RoomSummary.ToDepart,
                StayOver = x.RoomSummary.StayOver,
                EarlyArrival = x.RoomSummary.EarlyArrival,
                Arrived = x.RoomSummary.Arrived,
                ToArrive = x.RoomSummary.ToArrive,
                NoShow = x.RoomSummary.NoShow,
                Complementary = x.RoomSummary.Complementary,
                WalkIns = x.RoomSummary.WalkIns,
                RoomReservationCreated = x.RoomSummary.RoomReservationCreated,
                RoomReservationCancelled = x.RoomSummary.RoomReservationCancelled,
                BusinessDate = x.BusinessDate,
                PropertyCode = x.PropertyCode,
            }).ToList();
            return roomSummaries;
        }

        private List<DB.Models.ApmaModels.LedgerBalance> GetLedgerSummary(List<VDVI.DB.Models.ApmaModels.ManagementSummary> managementSummaryList)
        {

            List<DB.Models.ApmaModels.LedgerBalance> LedgerBalances = managementSummaryList.Select(x => new DB.Models.ApmaModels.LedgerBalance()
            {
                Reservations = x.LedgerBalance.Reservations,
                InHouseReservations = x.LedgerBalance.InHouseReservations,
                GroupReservations = x.LedgerBalance.GroupReservations,
                EventReservations = x.LedgerBalance.EventReservations,
                TotalTurnover = x.LedgerBalance.TotalTurnover,
                LodgingTurnover = x.LedgerBalance.LodgingTurnover,
                PaymentsDebitor = x.LedgerBalance.PaymentsDebitor,
                PaymentsCash = x.LedgerBalance.PaymentsCash,
                CityLedger = x.LedgerBalance.CityLedger,
                TotalTurnoverEx = x.LedgerBalance.TotalTurnoverEx,
                TotalTurnoverExSpecified = x.LedgerBalance.TotalTurnoverExSpecified,
                LodgingTurnoverEx = x.LedgerBalance.LodgingTurnoverEx,
                LodgingTurnoverExSpecified = x.LedgerBalance.LodgingTurnoverExSpecified,
                BusinessDate = x.BusinessDate,
                PropertyCode = x.PropertyCode

            }).ToList();
            return LedgerBalances;
        }

         
    }
}




//TODO : 1. API Calling, If calling success : 2. Param , 3. data format, 4. Save in database , 5. updated next lAst executed date, 
// Database : ExecutedDateTime
