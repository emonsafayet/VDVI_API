using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data; 
using VDVI.Repository.Repository.Interfaces;
using Newtonsoft.Json; 
using VDVI.Repository.Dtos.RoomSummary;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.Repository.Repository.Implementation
{
    public class HcsBIReservationDashboardRepository: IHcsBIReservationDashboardRepository
    {
        protected readonly IConfiguration _config;
        public HcsBIReservationDashboardRepository(IConfiguration config)
        {
            _config = config;
        } 
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("ApmaDb"));
            }
        }
        public string InsertReservation(List<ReservationDto> reservationDto)
        {
            string result = "";
            try
            {
                DataTable dt =
                JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(reservationDto));

                if (dt.Rows.Count > 0)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        using (SqlConnection con = new SqlConnection(dbConnection.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("spINSERT_hce_ReservationDashboard_Reservation_History"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@ReservationDashboard_Reservation_History_UDT", dt);
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

        public string InsertRooms(List<RoomsDto> reservationDto)
        {
            string result = "";
            try
            {
                DataTable dt =
                JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(reservationDto));

                if (dt.Rows.Count > 0)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        using (SqlConnection con = new SqlConnection(dbConnection.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("spINSERT_hce_ReservationDashboard_Rooms_History"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@ReservationDashboard_Rooms_History_UDT", dt);
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

        public string InsertRevenue(List<RevenueDto> revenueDto)
        {
            string result = "";
            try
            {
                DataTable dt =
                JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(revenueDto));

                if (dt.Rows.Count > 0)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        using (SqlConnection con = new SqlConnection(dbConnection.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("spINSERT_hce_ReservationDashboard_Revenue_History"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@ReservationDashboard_Revenue_History_UDT", dt);
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
        public string InsertOccupancy(List<OccupancyDto> occupancyDto)
        {
            string result = "";
            try
            {
                DataTable dt =
                JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(occupancyDto));

                if (dt.Rows.Count > 0)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        using (SqlConnection con = new SqlConnection(dbConnection.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("spINSERT_hce_ReservationDashboard_Occupancy_History"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@ReservationDashboard_Occupancy_History_UDT", dt);
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
