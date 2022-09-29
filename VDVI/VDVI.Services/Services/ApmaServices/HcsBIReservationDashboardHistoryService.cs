using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility; 
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.ApmaRepository.Interfaces;
using VDVI.DB.Dtos;
using VDVI.Services.Interfaces;

namespace VDVI.Services
{
    public class HcsBIReservationDashboardHistoryService : ApmaBaseService, IHcsBIReservationDashboardHistoryService
    {         
        public HcsBIReservationDashboardHistoryService(IHcsBIRevenueHistoryService hcsBIRevenueHistoryService, IHcsBIOccupancyHistoryService hcsBIOccupancyHistoryService,
                                           IHcsBIRoomsHistoryService hcsBIRoomsHistoryService, IHcsBIReservationHistoryService hcsBIReservationHistoryService)
        {
            _hcsBIRevenueHistoryService = hcsBIRevenueHistoryService;
            _hcsBIOccupancyHistoryService = hcsBIOccupancyHistoryService;
            _hcsBIReservationHistoryService = hcsBIReservationHistoryService;
            _hcsBIRoomsHistoryService = hcsBIRoomsHistoryService;
        }

        public IHcsBIRevenueHistoryService _hcsBIRevenueHistoryService ;
        public IHcsBIOccupancyHistoryService _hcsBIOccupancyHistoryService ;
        public IHcsBIReservationHistoryService _hcsBIReservationHistoryService ;
        public IHcsBIRoomsHistoryService _hcsBIRoomsHistoryService ;

        public async Task<Result<PrometheusResponse>> HcsBIReservationDashboardRepositoryAsyc(DateTime StartDate, DateTime EndDate)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                          async () =>
                          {
                              Authentication pmsAuthentication = GetApmaAuthCredential();

                              List<ReservationDashboardOccupancyHistoryDto> occupancies = new List<ReservationDashboardOccupancyHistoryDto>();
                              List<ReservationDashboardReservationHistoryDto> reservations = new List<ReservationDashboardReservationHistoryDto>();
                              List<ReservationDashboardRevenueHistoryDto> revenues = new List<ReservationDashboardRevenueHistoryDto>();
                              List<ReservationDashboardRoomsHistoryDto> rooms = new List<ReservationDashboardRoomsHistoryDto>();

                              foreach (string property in ApmaProperties)
                              {
                                  var res = await client.HcsBIReservationsDashboardAsync(pmsAuthentication, PropertyCode: property, StartDate: StartDate, EndDate: EndDate, "");

                                  List<BIDashboardData> dashboard = res.HcsBIReservationsDashboardResult.Dashboard.ToList();

                                  FormatSummaryObject(occupancies, reservations, revenues, rooms, dashboard, property);
                              }

                              // DB operation
                              var dboccupanciesRes = _hcsBIOccupancyHistoryService.BulkInsertWithProcAsync(occupancies);
                              var dbroomsRes = _hcsBIRoomsHistoryService.BulkInsertWithProcAsync(rooms);
                              var dbreservationsRes = _hcsBIReservationHistoryService.BulkInsertWithProcAsync(reservations);
                              var dbrevenuesRes = _hcsBIRevenueHistoryService.BulkInsertWithProcAsync(revenues);

                              return PrometheusResponse.Success("", "Data retrieval is successful");
                          },
                          exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                          {
                              DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                              RethrowException = false
                          });
        }


        private void FormatSummaryObject(List<ReservationDashboardOccupancyHistoryDto> occupancies, List<ReservationDashboardReservationHistoryDto> reservations, List<ReservationDashboardRevenueHistoryDto> revenues,

                  List<ReservationDashboardRoomsHistoryDto> rooms, List<BIDashboardData> dashboard, string propertyCode)
        {
            List<ReservationDashboardOccupancyHistoryDto> occupancys = dashboard.Select(x => new ReservationDashboardOccupancyHistoryDto()
            {
                Percentage = x.Occupancy.Percentage,
                RoomsSold = x.Occupancy.RoomsSold,
                AvailableRooms = x.Occupancy.AvailableRooms,
                DashboardDate = x.DashboardDate,
                PropertyCode = propertyCode,
            }).ToList();
            occupancies.AddRange(occupancys);

            List<ReservationDashboardReservationHistoryDto> reservation = dashboard.Select(x => new ReservationDashboardReservationHistoryDto()
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

            List<ReservationDashboardRevenueHistoryDto> revenue = dashboard.Select(x => new ReservationDashboardRevenueHistoryDto()
            {
                TypeA = x.Revenue.TypeA,
                TypeB = x.Revenue.TypeB,
                TypeC = x.Revenue.TypeC, 
                TypeD= x.Revenue.TypeD,
                TypeE= x.Revenue.TypeE,
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

            List<ReservationDashboardRoomsHistoryDto> room = dashboard.Select(x => new ReservationDashboardRoomsHistoryDto()
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
