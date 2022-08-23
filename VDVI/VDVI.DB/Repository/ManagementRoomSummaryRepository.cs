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


        public void InsertLedgerBalance(List<LedgerBalance> ledgerBalance)
        {
            try
            {
                DateTime localDate = DateTime.Now;
                foreach (var properties in ledgerBalance)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        dbConnection.Open();
                        string query = @"INSERT INTO [hce].[LedgerBalance](EntryDateTime,PropertyCode,BusinessDate,Reservations,InHouseReservations,GroupReservations,
                                   InHouseGroupReservations,EventReservations,TotalTurnover,LodgingTurnover,PaymentsDebitor,PaymentsCash,CityLedger)  VALUES
                                   (@BusinessDate, @PropertyCode, @BusinessDate,@Reservations, @InHouseReservations,@GroupReservations,  @InHouseGroupReservations,
                                   @EventReservations, @TotalTurnover, @LodgingTurnover,@PaymentsDebitor, @PaymentsCash , @CityLedger )";
                        dbConnection.Execute(query, properties);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertRoomSummary(List<RoomSummary> roomSummary)
        {
            try
            {
                foreach (var item in roomSummary)
                {
                    using (IDbConnection dbConnection = Connection)
                    {
                        dbConnection.Open();
                        string query = @"INSERT INTO [hce].[RoomSummary]  (EntryDateTime ,PropertyCode ,BusinessDate,InHouse ,DayUse ,LateArrival ,EarlyDeparture 
                                    ,Departed ,ToDepart ,StayOver ,  EarlyArrival  ,Arrived ,ToArrive ,
                                    NoShow ,Complementary ,WalkIns ,RoomReservationCreated  ,
                                    RoomReservationCancelled) VALUES
                                   (@BusinessDate,@PropertyCode,@BusinessDate,@InHouse,  @DayUse,
                                   @LateArrival,@EarlyDeparture,@Departed, 
                                   @ToDepart,@StayOver, @EarlyArrival,@Arrived, @ToArrive,@NoShow,@Complementary,
                                    @WalkIns,@RoomReservationCreated,@RoomReservationCancelled)";
                        dbConnection.Execute(query, item);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
