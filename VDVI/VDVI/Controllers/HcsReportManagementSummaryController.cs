using Microsoft.AspNetCore.Mvc;
using SOAPAppCore.Interfaces;
using System;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HcsReportManagementSummaryController : ControllerBase
    {
        private readonly IHcsReportManagementSummaryService _hcsBISourceStatisticsService;
        public HcsReportManagementSummaryController(IHcsReportManagementSummaryService hcsBISourceStatisticsService)
        {
            _hcsBISourceStatisticsService = hcsBISourceStatisticsService;
        }


        [HttpGet("GetReportManagement")]
        public async Task<IActionResult> GetReportManagement(/*string startDate, string endDate*/)
        {
            var dto = new RoomSummaryDto()
            {
                PropertyCode = "test code",
                BusinessDate = DateTime.UtcNow,
                InHouse = new Random().Next(),
                DayUse = new Random().Next(),
                LateArrival = new Random().Next(),
                EarlyDeparture = new Random().Next(),
                Departed = new Random().Next(),
                ToDepart = new Random().Next(),
                StayOver = new Random().Next(),
                EarlyArrival = new Random().Next(),
                Arrived = new Random().Next(),
                ToArrive = new Random().Next(),
                NoShow = new Random().Next(),
                Complementary = new Random().Next(),
                WalkIns = new Random().Next(),
                RoomReservationCreated = new Random().Next(),
                RoomReservationCancelled = new Random().Next()
            };

            var response = await _hcsBISourceStatisticsService.InsertAsync(dto);
            return Ok(response.Value);
        }
    }
}
