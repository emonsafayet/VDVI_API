using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Services.Interfaces;

namespace VDVI.Client.Controllers.ApmaControllers
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


        [HttpGet("PostReportManagement")]
        public async Task<IActionResult> PostReportManagement()
        {
            var response = await _hcsBISourceStatisticsService.ReportManagementSummaryAsync(Convert.ToDateTime("2018/01/01"), Convert.ToDateTime("2018/06/01"));

            return Ok(response);
        }
    }
}
