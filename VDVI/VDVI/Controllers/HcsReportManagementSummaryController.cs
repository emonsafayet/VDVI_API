using Microsoft.AspNetCore.Mvc;
using System;

namespace VDVI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HcsReportManagementSummaryController : ControllerBase
    {

        public HcsReportManagementSummaryController()
        {
        }


        [HttpPost("GetReportManagement")]
        public IActionResult GetReportManagement(string _startDate, string _endDate)
        {
            try
            {
                //_hcsReportManagementSummaryService.ManagementSummaryInsertManullyRoomAndLedger(_startDate, _endDate);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

    }
}
