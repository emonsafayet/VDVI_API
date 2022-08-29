
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; 
using SOAPService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VDVI.DB.IServices;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HcsReportManagementSummaryController : ControllerBase
    {

        public IHcsReportManagementSummaryService _hcsReportManagementSummaryService { get; }
        public HcsReportManagementSummaryController(IHcsReportManagementSummaryService hcsReportManagementSummaryService)
        {
            _hcsReportManagementSummaryService = hcsReportManagementSummaryService;
        }


        [HttpPost("GetReportManagement")]
        public IActionResult GetReportManagement(string _startDate, string _endDate)
        {
            try
            {
                _hcsReportManagementSummaryService.ManagementSummaryInsertManullyRoomAndLedger(_startDate, _endDate);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

    }
}
