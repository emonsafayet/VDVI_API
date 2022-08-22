
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SOAPAppCore.Interfaces.Apma;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.Json;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApmaController : ControllerBase
    {
        private readonly IReportManagementSummary _reportSummary;

        public ApmaController(IReportManagementSummary reportSummary)
        {
            _reportSummary = reportSummary;
        }

        [HttpPost("GetReportManagement")]
        public ActionResult<List<HcsReportManagementSummaryResponse>> GetReportManagement(DateTime startDate,DateTime enddate)
        {
            try
            { 
                List<HcsReportManagementSummaryResponse> res = _reportSummary.GetReportManagementSummaryFromApma(startDate, enddate);
               
                //TODO: Go to service for data binding 

                var jsonDatas = JsonConvert.SerializeObject(res, formatting: Newtonsoft.Json.Formatting.Indented);
 
               List<RerportManagementSummaryModel> reportManagementSummaries = JsonConvert.DeserializeObject<List<RerportManagementSummaryModel>>(jsonDatas);
                
               return Ok(reportManagementSummaries);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
            //return Ok();
        }
    }
}
