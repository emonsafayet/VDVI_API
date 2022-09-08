using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using VDVI.Repository.Repository.Interfaces;

namespace VDVI.Repository.Repository.Implementation
{
    public class HcsBIRatePlanStatisticsRepository : IHcsBIRatePlanStatisticsRepository
    {
        protected readonly IConfiguration _config;

        public HcsBIRatePlanStatisticsRepository(IConfiguration config)
        {
            _config = config;
        }

        //create an IDbConnection object called Connection to inito database connection
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("ApmaDb"));
            }
        }
    }
}
