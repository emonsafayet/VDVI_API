using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Repository.Repository.Interfaces
{
    public interface IHcsBIReservationDashboardRepository
    {
        string InsertReservation(List<ReservationDto> reservationDto);
        string InsertRooms(List<RoomsDto> reservationDto);
        string InsertRevenue(List<RevenueDto> revenueDto);
        string InsertOccupancy(List<OccupancyDto> occupancyDto);

    }
}
