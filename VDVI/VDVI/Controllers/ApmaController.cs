
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SOAPAppCore.Interfaces.Apma;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.Json;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApmaController : ControllerBase
    {
        private readonly IReportManagementSummary _reportSummary;

        public ApmaController()
        {
           
        }

        [HttpPost("GetReportManagement")]
        //public ActionResult<List<HcsReportManagementSummaryResponse>> GetReportManagement(DateTime startDate,DateTime enddate)
        public IActionResult GetReportManagement(DateTime startDate, DateTime enddate)
        {
            try
            {
                              

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

          }
}
