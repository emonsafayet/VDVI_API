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
        public async Task<IActionResult> GetReportManagement(/*DateTime startDate, DateTime endDate*/)
        {
            var response = await _hcsBISourceStatisticsService.ReportManagementSummaryAsync(Convert.ToDateTime("2018/01/01"), Convert.ToDateTime("2018/06/01"));

            return Ok(response);
        }
    }
}
