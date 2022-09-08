using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.RoomSummary
{
    public class ReservationDto
    {
        public int CreatedReservations { get; set; }
        public int CreatedRoomNights { get; set; }
        public int StayOvers { get; set; }
        public int CancelledReservations { get; set; }
        public int CancelledRoomNights { get; set; }
        public int CheckIns { get; set; }
        public int CheckedIn { get; set; }
        public int CheckOuts { get; set; }
        public int CheckedOut { get; set; }
        public int DayUseCheckIns { get; set; }
        public int DayUseCheckOuts { get; set; }
        public double AverageLengthOfStay { get; set; }
        public double AverageLeadTime { get; set; }
        public int NoShowProcessed { get; set; }
        public int NoShowUnprocessed { get; set; }
    }

}
