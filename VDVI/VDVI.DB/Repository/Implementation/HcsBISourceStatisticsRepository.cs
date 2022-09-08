using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VDVI.DB.IRepository;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.Repository
{
    public class HcsBISourceStatisticsRepository : IHcsBISourceStatisticsRepository
    {
        protected readonly IConfiguration _config;

        public HcsBISourceStatisticsRepository(IConfiguration config)
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
        public string InsertHcsBISourceStatisticsHistory(List<SourceStatistic> sourceStatistics)
        {
            string result = "";
            try
            {
                DataTable dt = new DataTable();

                if (sourceStatistics.Count > 0)
                    dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(sourceStatistics));

                if (dt.Rows.Count > 0)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        using (SqlConnection con = new SqlConnection(dbConnection.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("spINSERT_hce_SourceStatistics_History"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@SourceStatistics_History_UDT", dt);
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
