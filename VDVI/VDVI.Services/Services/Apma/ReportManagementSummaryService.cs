using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using SOAPAppCore.Interfaces;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.DB.Dtos.Accounts;
using VDVI.DB.Dtos.RoomSummary;
using VDVI.Repository.Repository;

namespace SOAPAppCore.Services.Apma
{
    public class ReportManagementSummaryService : ApmaBaseService, IReportManagementSummaryService
    {
        private readonly IScheduleManagementRepository _scheduleManagementRepository;

        public ReportManagementSummaryService(IScheduleManagementRepository scheduleManagementRepository)
        {
            _scheduleManagementRepository = scheduleManagementRepository;
        }

        public async Task<Result<PrometheusResponse>> ReportManagementSummaryAsync(DateTime StartDate, DateTime EndDate)
        {

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    Authentication pmsAuthentication = GetApmaAuthCredential();

                    List<RoomSummaryDto> roomSummaries = new List<RoomSummaryDto>();
                    List<LedgerBalanceDto> ledgerBalances = new List<LedgerBalanceDto>();

                    foreach (string property in ApmaProperties)
                    {
                        var res = await client.HcsReportManagementSummaryAsync(pmsAuthentication, PropertyCode: property, StartDate: StartDate, EndDate: EndDate, "");

                        List<ManagementSummary> managementSummary = res.HcsReportManagementSummaryResult.ManagementSummaries.ToList();

                        FormatSummaryObject(roomSummaries, ledgerBalances, managementSummary, property);
                    }


                    // DB operation
                    var dbroomSummariesRes = _scheduleManagementRepository.RoomSummaryRepository.BulkInsertAsync(roomSummaries);
                    //var dbledgerBalancesRes = _roomSummaryRepository.InsertLedgerBalance(ledgerBalances);

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }


        public async Task<Result<PrometheusResponse>> InsertAsync(RoomSummaryDto dto)
        {

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                   
                    
                    var dbroomSummariesRes = _scheduleManagementRepository.RoomSummaryRepository.InsertAsync(dto);

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        private void FormatSummaryObject(List<RoomSummaryDto> roomSummaries, List<LedgerBalanceDto> ledgerBalances, List<ManagementSummary> managementSummary, string propertyCode)
        {
            List<RoomSummaryDto> rooms = managementSummary.Select(x => new RoomSummaryDto()
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
                PropertyCode = propertyCode,
            }).ToList();
            roomSummaries.AddRange(rooms);


            List<LedgerBalanceDto> ledgerList = managementSummary.Select(x => new LedgerBalanceDto()
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
                PropertyCode = propertyCode

            }).ToList();

            ledgerBalances.AddRange(ledgerList);
        }

        public Task<Result<PrometheusResponse>> BulkInsertAsync(RoomSummaryDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
