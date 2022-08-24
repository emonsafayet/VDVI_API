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
using VDVI.Services.IServices;

namespace VDVI.DB.Services
{
    public class ReportManagementSummariesService : IReportManagementSummariesService
    {
         
        private readonly IReportManagementSummary _reportSummary;
        private readonly IReportManagementDataInsertionService _reportManagementDataInsertionService;
        private readonly IApmaTaskSchedulerService _apmaTaskSchedulerService;

        private List<DB.Models.ApmaModels.RoomSummary> roomSummaryList=new List<DB.Models.ApmaModels.RoomSummary>();
        private List<DB.Models.ApmaModels.LedgerBalance> ledgerBalanceList = new List<DB.Models.ApmaModels.LedgerBalance>();

         
         
        //DateTime initialDate = new DateTime(2018, 01, 01); //This initial Date comes from appsettings.
        //DateTime StartDate = new DateTime(2018, 01, 01);
        //DateTime Enddate = DateTime.Now;//new DateTime(2018, 01, 01);


        public ReportManagementSummariesService(IReportManagementSummary reportSummary, 
            IReportManagementDataInsertionService reportManagementDataInsertionService,IApmaTaskSchedulerService apmaTaskSchedulerService)
        { 
            _reportSummary = reportSummary;
            _reportManagementDataInsertionService = reportManagementDataInsertionService;
            _apmaTaskSchedulerService = apmaTaskSchedulerService;
        }

       
        public void GetManagementData()
        {
            //hangfire; Algorithm 

            string startDate = "2019/01/01";
            string enddate = "2019/01/15";
            DateTime StartDate = Convert.ToDateTime(startDate);
            DateTime Enddate = Convert.ToDateTime(enddate);

            try
            {
                List<HcsReportManagementSummaryResponse> res = _reportSummary.GetReportManagementSummaryFromApma(StartDate, Enddate);

                var jsonDatas = JsonConvert.SerializeObject(res, formatting: Newtonsoft.Json.Formatting.Indented);

                List<RerportManagementSummaryModel> reportManagementSummaries = JsonConvert.DeserializeObject<List<RerportManagementSummaryModel>>(jsonDatas);

                List<HcsReportManagementSummaryResult> filterreportManagementSummaries = new List<HcsReportManagementSummaryResult>();

                filterreportManagementSummaries = reportManagementSummaries.Where(r => r.HcsReportManagementSummaryResult.Success == true).ToList().Select(x => x.HcsReportManagementSummaryResult).ToList();

                List<VDVI.DB.Models.ApmaModels.ManagementSummary> managementSummaryList = new List<DB.Models.ApmaModels.ManagementSummary>();


                if (filterreportManagementSummaries.Count != 0)
                {
                    managementSummaryList = GetmanagementSummaryList(filterreportManagementSummaries);

                    roomSummaryList = GetRoomSummary(managementSummaryList);

                    ledgerBalanceList = GetLedgerSummary(managementSummaryList);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }  

        private List<DB.Models.ApmaModels.ManagementSummary> GetmanagementSummaryList(List<VDVI.DB.Models.ApmaModels.HcsReportManagementSummaryResult> filterreportManagementSummaries)
        {
            var managementSummaryList = new List<Models.ApmaModels.ManagementSummary>();
            try
            {
                var result = filterreportManagementSummaries.Select(x => x);

                if (result!=null)
                {
                    // need to optimize; using linq will be better;
                    foreach (var item in result)
                    {
                        var tempList = item.ManagementSummaries.Select(x => new DB.Models.ApmaModels.ManagementSummary()
                        {
                            PropertyCode = item.PropertyCode,
                            BusinessDate = x.BusinessDate,
                            RoomSummary = x.RoomSummary,
                            LedgerBalance = x.LedgerBalance
                        }).ToList();
                        managementSummaryList.AddRange(tempList);
                    }
                }
               
                return managementSummaryList;
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        private List<DB.Models.ApmaModels.RoomSummary> GetRoomSummary(List<VDVI.DB.Models.ApmaModels.ManagementSummary> managementSummaryList)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }           
        }

        private List<DB.Models.ApmaModels.LedgerBalance> GetLedgerSummary(List<VDVI.DB.Models.ApmaModels.ManagementSummary> managementSummaryList)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            } 
        }

        public void InsertReportManagenetRoomAndLedgerData()
        {

            GetManagementData();
            if(ledgerBalanceList.Count!=0) _reportManagementDataInsertionService.InsertLedgerBalance(ledgerBalanceList);
            if(roomSummaryList.Count!=0) _reportManagementDataInsertionService.InsertRoomSummary(roomSummaryList);

            //Task Scheduler 
           // RenderTaskScheduling();

        }

        //private void RenderTaskScheduling()
        //{            
        //    var endDate=_apmaTaskSchedulerService.GetTaskScheduler("HcsReportManagementSummary");

        //    if (endDate==null)
        //    {
        //        StartDate = initialDate; // it will come from appsettig initialDate
        //        Enddate =DateTime.Now; // it will come from appsetting time difference

        //        _apmaTaskSchedulerService.InsertOrUpdateTaskScheduleDatetime("HcsReportManagementSummary", StartDate, Enddate,0);
        //    }
        //    else
        //    {
        //        StartDate = Enddate.AddMilliseconds(1); // it will come from appsettig initialDate
        //        Enddate = StartDate.AddMinutes(2); // it will come from appsetting time difference

        //        _apmaTaskSchedulerService.InsertOrUpdateTaskScheduleDatetime("HcsReportManagementSummary", StartDate, Enddate, 1);
        //    }


        //}
       

    }
}




//TODO : 1. API Calling,
//If calling success :
//2. Param ,
//3. data format,
//4. Save in database ,
//5. updated next lAst executed date, 
// Database : ExecutedDateTime
