using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using VDVI.Repository.Repository.Interfaces;
using Newtonsoft.Json;
using VDVI.Repository.Dtos.RoomSummary;
using VDVI.Repository.Dtos.Accounts;

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
        public string InsertRatePlanStatisticHistory(List<DbRatePlanStatistic> ratePlanStatisticDto)
        {
            string result = "";
            try
            {
                DataTable dt =
                JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(ratePlanStatisticDto));

                if (dt.Rows.Count > 0)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        using (SqlConnection con = new SqlConnection(dbConnection.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("spINSERT_hce_RatePlanStatistics_History"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@RatePlanStatistics_History_UDT", dt);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                        }
                    }

                }
                result = "Successfull";

            }
            catch (Exception ex)
            {
                result = ex.Message;
                throw ex;
            }
            return result;
        }
    }
}
