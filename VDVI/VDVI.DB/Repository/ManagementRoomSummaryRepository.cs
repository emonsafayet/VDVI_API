using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.IRepository;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.Repository
{
    public class ManagementRoomSummaryRepository : IManagementRoomSummaryRepository
    { 
        protected readonly IConfiguration _config;

        public ManagementRoomSummaryRepository(IConfiguration config)
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


        public string InsertLedgerBalance(List<LedgerBalance> ledgerBalance)
        {
            string result = "";
            try
            {
                var param = new DynamicParameters();
                foreach (var properties in ledgerBalance)
                {
                
                    using (IDbConnection dbConnection = Connection)
                    {
                        param.Add("@PropertyCode", properties.PropertyCode);
                        param.Add("@BusinessDate", properties.BusinessDate);
                        param.Add("@Reservations", properties.Reservations);
                        param.Add("@InHouseReservations", properties.InHouseReservations);
                        param.Add("@GroupReservations", properties.GroupReservations);
                        param.Add("@InHouseGroupReservations", properties.InHouseGroupReservations);
                        param.Add("@EventReservations", properties.EventReservations);
                        param.Add("@TotalTurnover", properties.TotalTurnover);
                        param.Add("@LodgingTurnover", properties.LodgingTurnover);
                        param.Add("@PaymentsDebitor", properties.PaymentsDebitor);
                        param.Add("@PaymentsCash", properties.PaymentsCash);
                        param.Add("@CityLedger", properties.CityLedger);
                        dbConnection.Open(); 
                        dbConnection.Query("spINSERT_hce_LedgerBalance", param,commandType:CommandType.StoredProcedure);
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

        public string InsertRoomSummary(List<RoomSummary> roomSummary)
        {
            string result = "";
            try
            {
                var param = new DynamicParameters();
                foreach (var properties in roomSummary)
                {
                    using (IDbConnection dbConnection = Connection)
                    {                       

                        param.Add("@PropertyCode", properties.PropertyCode);
                        param.Add("@BusinessDate", properties.BusinessDate);
                        param.Add("@InHouse", properties.InHouse);
                        param.Add("@DayUse", properties.DayUse);
                        param.Add("@LateArrival", properties.LateArrival);
                        param.Add("@EarlyDeparture", properties.EarlyDeparture);
                        param.Add("@Departed", properties.Departed);
                        param.Add("@ToDepart", properties.ToDepart);
                        param.Add("@StayOver", properties.StayOver);
                        param.Add("@EarlyArrival", properties.EarlyArrival);
                        param.Add("@Arrived", properties.Arrived);
                        param.Add("@ToArrive", properties.ToArrive);
                        param.Add("@NoShow", properties.NoShow);
                        param.Add("@Complementary", properties.Complementary);
                        param.Add("@WalkIns", properties.WalkIns);
                        param.Add("@RoomReservationCreated", properties.RoomReservationCreated);
                        param.Add("@RoomReservationCancelled", properties.RoomReservationCancelled);

                        dbConnection.Open();
                        dbConnection.Query("spINSERT_hce_RoomSummary", param, commandType: CommandType.StoredProcedure);                         
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
