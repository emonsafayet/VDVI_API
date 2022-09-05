﻿using Hangfire;
using Microsoft.Extensions.Configuration;
using NCrontab;
using Newtonsoft.Json;
using Quartz;
using SOAPAppCore.Interfaces; 
using SOAPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VDVI.Common;
using VDVI.DB.IRepository;
using VDVI.DB.IServices;
using VDVI.DB.Models.ApmaModels;
using VDVI.DB.Models.Common;
using VDVI.Services.IServices;

namespace VDVI.DB.Services
{
    public class HcsReportManagementSummaryService : IHcsReportManagementSummaryService
    {

        private readonly IReportManagementSummaryService _reportSummary;
        private readonly IConfiguration _config;
        private readonly IHcsReportManagementSummaryDataInsertionService _reportManagementDataInsertionService;
        private readonly IApmaTaskSchedulerService _apmaTaskSchedulerService;

        private List<DB.Models.ApmaModels.RoomSummary> roomSummaryList = new List<DB.Models.ApmaModels.RoomSummary>();
        private List<DB.Models.ApmaModels.LedgerBalance> ledgerBalanceList = new List<DB.Models.ApmaModels.LedgerBalance>();

        DateTime StartDate = new DateTime();
        DateTime Enddate = new DateTime();


        public HcsReportManagementSummaryService(IReportManagementSummaryService reportSummary , IConfiguration config,
            IHcsReportManagementSummaryDataInsertionService reportManagementDataInsertionService, IApmaTaskSchedulerService apmaTaskSchedulerService)
        {
            _reportSummary = reportSummary;
            _config = config;
            _reportManagementDataInsertionService = reportManagementDataInsertionService;
            _apmaTaskSchedulerService = apmaTaskSchedulerService;
        }

        //This is working for hangfire;     
        
        public void GetManagementSummary()
        {

            try
            {

                GetManagementSummaryData("", "", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetManagementSummaryData(string _startDate, string _endDate, bool isManual = false)
        {
            if (isManual)
            {
                StartDate = Convert.ToDateTime(_startDate);
                Enddate = Convert.ToDateTime(_endDate);
            }             
            List<HcsReportManagementSummaryResponse> res = _reportSummary.GetReportManagementSummary(StartDate, Enddate);
            var jsonDatas = JsonConvert.SerializeObject(res, formatting: Newtonsoft.Json.Formatting.Indented);

            List<HcsReportManagementSummaryModel> reportManagementSummaries = JsonConvert.DeserializeObject<List<HcsReportManagementSummaryModel>>(jsonDatas);

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
        private List<DB.Models.ApmaModels.ManagementSummary> GetmanagementSummaryList(List<VDVI.DB.Models.ApmaModels.HcsReportManagementSummaryResult> filterreportManagementSummaries)
        {
            var managementSummaryList = new List<Models.ApmaModels.ManagementSummary>();
            try
            {
                var result = filterreportManagementSummaries.Select(x => x);

                if (result != null)
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
        public void ManagementSummaryInsertRoomAndLedger()
        {
            var res = GetStartAndEndDate();

            var LedgerSummeryResult = "";
            var RoomSummeryResult = "";

            GetManagementSummary();
            if (ledgerBalanceList.Count > 0)
                RoomSummeryResult = _reportManagementDataInsertionService.InsertLedgerBalance(ledgerBalanceList);

            if (roomSummaryList.Count > 0)
                LedgerSummeryResult = _reportManagementDataInsertionService.InsertRoomSummary(roomSummaryList);

            if (RoomSummeryResult == "Successfull" && LedgerSummeryResult == "Successfull")
            {
                //Task Scheduler 
                if (res == null)
                    RenderTaskScheduling("Insert");
                else
                    RenderTaskScheduling("Update");
            }
        }
        
        //Scheduler Configuration from appsetting.json 
        private JobTaskScheduler GetStartAndEndDate()
        {
            string resultDate = _config.GetSection("ApmaServiceDateConfig").GetSection("initialStartDate").Value;
            DateTime apmaInitialDate = Convert.ToDateTime(resultDate);

            var dayDiffernce = _config.GetSection("ApmaServiceDateConfig").GetSection("DayDifferenceReportManagementRoomAndLedgerSummary").Value;

            //Check from the Database by method Name, if there have any existing value or not ;
            JobTaskScheduler taskScheduleEndDate = _apmaTaskSchedulerService.GetTaskScheduler("HcsReportManagementSummary");

            if (taskScheduleEndDate == null)
            {
                StartDate = apmaInitialDate;
                Enddate = StartDate.AddDays(Convert.ToInt32(dayDiffernce));
            }
            else if (taskScheduleEndDate.LastExecutionDate != null)
            {
                StartDate = Convert.ToDateTime(taskScheduleEndDate.LastExecutionDate);
                Enddate = StartDate.AddDays(Convert.ToInt32(dayDiffernce));
            }
            return taskScheduleEndDate;
        }

        //Into the JobTaskScheduler table is Inserting or Updating data; after successfully Entry on RoomSummary and LedgerSummary table;
        private void RenderTaskScheduling(string actionReulst)
        {
            if (actionReulst == "Insert") //1 for Insert
            {
                _apmaTaskSchedulerService.InsertOrUpdateTaskScheduleDatetime("HcsReportManagementSummary", Enddate, 0);
            }
            else
            {
                _apmaTaskSchedulerService.InsertOrUpdateTaskScheduleDatetime("HcsReportManagementSummary", Enddate, 1);
            }

        }

        //Insert Manually ReportManagementRoomAndLedgerSummery
        public string ManagementSummaryInsertManullyRoomAndLedger(string _startDate, string _endDate)
        {
            var message = "";
            try
            {
                GetManagementSummaryData(_startDate, _endDate, true);

                if (roomSummaryList.Count > 0 && ledgerBalanceList.Count > 0)
                {
                    _reportManagementDataInsertionService.InsertRoomSummary(roomSummaryList);
                    _reportManagementDataInsertionService.InsertLedgerBalance(ledgerBalanceList);
                    message = "Room and Ledger Summary Inserted Successfully";
                }
                return message;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

       
    }
}