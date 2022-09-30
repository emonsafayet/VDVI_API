using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Services.Interfaces;

namespace VDVI.Services.Services.ApmaServices
{
    public class HcsBIReservationDashboardFutureService : ApmaBaseService, IHcsBIReservationDashboardFutureService
    {
        public HcsBIReservationDashboardFutureService(IHcsBIRevenueFutureService hcsBIRevenueFutureService, IHcsBIOccupancyFutureService hcsBIOccupancyFutureService,
                                           IHcsBIRoomsFutureService hcsBIRoomsFutureService, IHcsBIReservationFutureService hcsBIReservationFutureService)
        {
            _hcsBIRevenueFutureService = hcsBIRevenueFutureService;
            _hcsBIOccupancyFutureService = hcsBIOccupancyFutureService;
            _hcsBIReservationFutureService = hcsBIReservationFutureService;
            _hcsBIRoomsFutureService = hcsBIRoomsFutureService;
        }

        public IHcsBIRevenueFutureService _hcsBIRevenueFutureService;
        public IHcsBIOccupancyFutureService _hcsBIOccupancyFutureService;
        public IHcsBIReservationFutureService _hcsBIReservationFutureService;
        public IHcsBIRoomsFutureService _hcsBIRoomsFutureService;

        public async Task<Result<PrometheusResponse>> HcsBIReservationDashboardRepositoryAsyc(DateTime lastExecutionDate, int dayDifference)
        {
            DateTime nextExecutionDate = lastExecutionDate.AddMonths(12).AddSeconds(1);
            DateTime tempDate = lastExecutionDate;

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                          async () =>
                          {
                              Authentication pmsAuthentication = GetApmaAuthCredential();

                              List<ReservationDashboardOccupancyFutureDto> occupancies = new List<ReservationDashboardOccupancyFutureDto>();
                              List<ReservationDashboardReservationFutureDto> reservations = new List<ReservationDashboardReservationFutureDto>();
                              List<ReservationDashboardRevenueFutureDto> revenues = new List<ReservationDashboardRevenueFutureDto>();
                              List<ReservationDashboardRoomsFutureDto> rooms = new List<ReservationDashboardRoomsFutureDto>();

                              while (tempDate < nextExecutionDate)
                              {
                                  var endDate = tempDate.AddDays(dayDifference);
                                  endDate = endDate > nextExecutionDate ? nextExecutionDate : endDate;

                                  for (int i = 0; i < ApmaProperties.Length; i++)
                                  {
                                      var propertyCode = ApmaProperties[i];
                                      var res = await client.HcsBIReservationsDashboardAsync(pmsAuthentication, PropertyCode: propertyCode, StartDate: tempDate, EndDate: endDate, "");

                                      List<BIDashboardData> dashboard = res.HcsBIReservationsDashboardResult.Dashboard.ToList();

                                      FormatSummaryObject(occupancies, reservations, revenues, rooms, dashboard, propertyCode);
                                  }
                                  tempDate = tempDate.AddDays(dayDifference).AddSeconds(1);
                              }
                              // DB operation
                              var dboccupanciesRes = _hcsBIOccupancyFutureService.BulkInsertWithProcAsync(occupancies);
                              var dbroomsRes = _hcsBIRoomsFutureService.BulkInsertWithProcAsync(rooms);
                              var dbreservationsRes = _hcsBIReservationFutureService.BulkInsertWithProcAsync(reservations);
                              var dbrevenuesRes = _hcsBIRevenueFutureService.BulkInsertWithProcAsync(revenues);

                              return PrometheusResponse.Success("", "Data retrieval is successful");
                          },
                          exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                          {
                              DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                              RethrowException = false
                          });
        }


        private void FormatSummaryObject(List<ReservationDashboardOccupancyFutureDto> occupancies, List<ReservationDashboardReservationFutureDto> reservations, List<ReservationDashboardRevenueFutureDto> revenues,

                  List<ReservationDashboardRoomsFutureDto> rooms, List<BIDashboardData> dashboard, string propertyCode)
        {
            List<ReservationDashboardOccupancyFutureDto> occupancys = dashboard.Select(x => new ReservationDashboardOccupancyFutureDto()
            {
                Percentage = x.Occupancy.Percentage,
                RoomsSold = x.Occupancy.RoomsSold,
                AvailableRooms = x.Occupancy.AvailableRooms,
                DashboardDate = x.DashboardDate,
                PropertyCode = propertyCode,
            }).ToList();
            occupancies.AddRange(occupancys);

            List<ReservationDashboardReservationFutureDto> reservation = dashboard.Select(x => new ReservationDashboardReservationFutureDto()
            {
                CreatedReservations = x.Reservation.CreatedReservations,
                CreatedRoomNights = x.Reservation.CreatedRoomNights,
                StayOvers = x.Reservation.CreatedReservations,
                CancelledReservations = x.Reservation.CancelledReservations,
                CancelledRoomNights = x.Reservation.CancelledRoomNights,
                CheckIns = x.Reservation.CheckIns,
                CheckedIn = x.Reservation.CheckedIn,
                CheckOuts = x.Reservation.CheckOuts,
                CheckedOut = x.Reservation.CheckedOut,
                DayUseCheckIns = x.Reservation.DayUseCheckIns,
                DayUseCheckOuts = x.Reservation.DayUseCheckOuts,
                AverageLengthOfStay = x.Reservation.AverageLengthOfStay,
                AverageLeadTime = x.Reservation.AverageLeadTime,
                NoShowProcessed = x.Reservation.NoShowProcessed,
                NoShowUnprocessed = x.Reservation.NoShowUnprocessed,
                DashboardDate = x.DashboardDate,
                PropertyCode = propertyCode,
            }).ToList();
            reservations.AddRange(reservation);

            List<ReservationDashboardRevenueFutureDto> revenue = dashboard.Select(x => new ReservationDashboardRevenueFutureDto()
            {
                TypeA = x.Revenue.TypeA,
                TypeB = x.Revenue.TypeB,
                TypeC = x.Revenue.TypeC,
                TypeD = x.Revenue.TypeD,
                TypeE = x.Revenue.TypeE,
                TypeF = x.Revenue.TypeF,
                Undefined = x.Revenue.Undefined,
                AverageDailyRate = x.Revenue.AverageDailyRate,
                RevenuePerAvailableRoom = x.Revenue.RevenuePerAvailableRoom,
                SpentPerAvailableRoom = x.Revenue.SpentPerAvailableRoom,
                TotalRevenue = x.Revenue.TotalRevenue,
                DashboardDate = x.DashboardDate,
                PropertyCode = propertyCode,
            }).ToList();
            revenues.AddRange(revenue);

            List<ReservationDashboardRoomsFutureDto> room = dashboard.Select(x => new ReservationDashboardRoomsFutureDto()
            {
                Inventory = x.Rooms.Inventory,
                OutOfInventory = x.Rooms.OutOfInventory,
                OutOfOrder = x.Rooms.OutOfOrder,
                DashboardDate = x.DashboardDate,
                PropertyCode = propertyCode,
            }).ToList();
            rooms.AddRange(room);
        }
    }
}
