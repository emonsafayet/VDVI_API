using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VDVI.DB.Dtos;
using VDVI.DB.IRepository;

namespace VDVI.DB.Repository
{
    public class HcsReportManagementSummaryRepository : IHcsReportManagementSummaryRepository
    {
        protected readonly IConfiguration _config;

        public HcsReportManagementSummaryRepository(IConfiguration config)
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

        public string InsertLedgerBalance(List<LedgerBalanceDto> ledgerBalance)
        {
            string result = "";
            try
            {
                DataTable dt =
                JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(ledgerBalance));

                if (dt.Rows.Count > 0)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        using (SqlConnection con = new SqlConnection(dbConnection.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("spINSERT_hce_LedgerBalance"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@ManagementSummary_LedgerBalance_UDT", dt);
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

        public string InsertRoomSummary(List<RoomSummaryDto> roomSummary)
        {
            string result = "";
            try
            {
                DataTable dt =
                    JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(roomSummary));

                if (dt.Rows.Count > 0)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        using (SqlConnection con = new SqlConnection(dbConnection.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("spINSERT_hce_RoomSummary"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@ManagementSummary_RoomSummary_UDT", dt);
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
