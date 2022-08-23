
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
using System.Threading.Tasks;
using VDVI.DB.IServices;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApmaController : ControllerBase
    {
        public IReportManagementSummariesService _roomManagementSummariesService { get; }
        public ApmaController(IReportManagementSummariesService roomManagementSummariesService)
        {
            _roomManagementSummariesService = roomManagementSummariesService;
        }


        [HttpPost("GetReportManagement")]
        public  IActionResult GetReportManagement()
        {
            try
            {
                _roomManagementSummariesService.GetManagementData();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

          }
}
