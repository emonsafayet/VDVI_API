using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using SOAPAppCore.Services;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.RoomSummary;
using VDVI.Repository.Interfaces;
using VDVI.Services.Interfaces.Apma;

namespace VDVI.Services.Services.Apma
{
    public class HcsBIReservationDashboardService : ApmaBaseService, IHcsBIReservationDashboardService
    {

        private readonly IHcsBIReservationDashboardRepository _hcsBIReservationDashboardRepository;
        public HcsBIReservationDashboardService(IHcsBIReservationDashboardRepository hcsBIReservationDashboardRepository)
        {
            _hcsBIReservationDashboardRepository = hcsBIReservationDashboardRepository;
        }

        public async Task<Result<PrometheusResponse>> HcsBIReservationDashboardRepositoryAsyc(DateTime StartDate, DateTime EndDate)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                          async () =>
                          {
                              Authentication pmsAuthentication = GetApmaAuthCredential();

                              List<OccupancyHistoryDto> occupancies = new List<OccupancyHistoryDto>();
                              List<ReservationHistoryDto> reservations = new List<ReservationHistoryDto>();
                              List<DbRevenueHistory> revenues = new List<DbRevenueHistory>();
                              List<RoomsHistoryDto> rooms = new List<RoomsHistoryDto>();

                              foreach (string property in ApmaProperties)
                              {
                                  var res = await client.HcsBIReservationsDashboardAsync(pmsAuthentication, PropertyCode: property, StartDate: StartDate, EndDate: EndDate, "");

                                  List<BIDashboardData> dashboard = res.HcsBIReservationsDashboardResult.Dashboard.ToList();

                                  FormatSummaryObject(occupancies, reservations, revenues, rooms, dashboard, property);
                              }

                              // DB operation
                              var dboccupanciesRes = _hcsBIReservationDashboardRepository.InsertOccupancy(occupancies);
                              var dbroomsRes = _hcsBIReservationDashboardRepository.InsertRooms(rooms);
                              var dbreservationsRes = _hcsBIReservationDashboardRepository.InsertReservation(reservations);
                              var dbrevenuesRes = _hcsBIReservationDashboardRepository.InsertRevenue(revenues);

                              return PrometheusResponse.Success("", "Data retrieval is successful");
                          },
                          exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                          {
                              DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                              RethrowException = false
                          });
        }


        private void FormatSummaryObject(List<OccupancyHistoryDto> occupancies, List<ReservationHistoryDto> reservations, List<DbRevenueHistory> revenues,

                  List<RoomsHistoryDto> rooms, List<BIDashboardData> dashboard, string propertyCode)
        {
            List<OccupancyHistoryDto> occupancys = dashboard.Select(x => new OccupancyHistoryDto()
            {
                Percentage = x.Occupancy.Percentage,
                RoomsSold = x.Occupancy.RoomsSold,
                AvailableRooms = x.Occupancy.AvailableRooms,
                DashboardDate = x.DashboardDate,
                PropertyCode = propertyCode,
            }).ToList();
            occupancies.AddRange(occupancys);

            List<ReservationHistoryDto> reservation = dashboard.Select(x => new ReservationHistoryDto()
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

            List<DbRevenueHistory> revenue = dashboard.Select(x => new DbRevenueHistory()
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

            List<RoomsHistoryDto> room = dashboard.Select(x => new RoomsHistoryDto()
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
