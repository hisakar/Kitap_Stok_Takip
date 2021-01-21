using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Services
{
    public class DemandService : IDemandService
    {

        private readonly string _connectionString;
        public DemandService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        public IEnumerable<Demand> GetAllDemands()
        {
            List<Demand> demands = new List<Demand>();

            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        command.BindByName = true;
                        command.CommandText = "SELECT * FROM DEMANDS D JOIN MEMBERS M ON D.MEMBER_ID = M.MEMBER_ID ";
                        OracleDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            
                            User user = new User
                            {
                                UserId = Convert.ToInt32(reader["MEMBER_ID"]),
                                Username = reader["USER_NAME"].ToString()
                            };

                            Demand demand = new Demand
                            {
                                DemandId = Convert.ToInt32(reader["DEMAND_ID"]),
                                User = user,
                                BookName = reader["BOOK_NAME"].ToString(),
                                Writer = reader["WRITER"].ToString(),
                                RequestedDate = reader["REQUESTED_DATE"].ToString()
                            };
                            demands.Add(demand);
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return demands;

        }

        public Demand AddDemand(Demand demand)
        {
            try
            {

                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        OracleParameter[] prm = new OracleParameter[4];

                        prm[0] = command.Parameters.Add("memberId", OracleDbType.Decimal,
                           demand.User.UserId, ParameterDirection.Input);
                        prm[2] = command.Parameters.Add("bookName", OracleDbType.Varchar2,
                          demand.BookName, ParameterDirection.Input);
                        prm[3] = command.Parameters.Add("writer", OracleDbType.Varchar2,
                          demand.Writer, ParameterDirection.Input);


                        var sql = "INSERT INTO DEMANDS(" +         
                            "MEMBER_ID," +
                            "BOOK_NAME," +
                            "WRITER," +
                            "REQUESTED_DATE) " +
                            "VALUES(:memberId, :bookName, :writer, TO_CHAR(sysdate,'DD/MM/YYYY HH24:MI:SS'))";

                        command.BindByName = true;
                        command.CommandText = sql;
                        command.ExecuteNonQuery();


                    }
                }
            }
            catch
            {
                throw;
            }
            return demand;
        }

        public void DeleteDemandsByUserId(int userId) //not used yet
        {
            throw new NotImplementedException();
        }

        public void DeleteSingleDemand(Demand demand)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("memberId", demand.User.UserId);
                    command.Parameters.Add("demandId", demand.DemandId);
                    command.CommandText = "Delete From DEMANDS Where MEMBER_ID = :memberId AND DEMAND_ID = :demandId";
                    command.ExecuteNonQuery();

                }
            }
        }

        public IEnumerable<Demand> GetAllDemandsByUserId(int userId)
        {
            List<Demand> demands = new List<Demand>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;

                    command.Parameters.Add("userId", userId);
                    command.CommandText = "SELECT * " +
                        "FROM DEMANDS D " +
                        "JOIN MEMBERS M ON D.MEMBER_ID = M.MEMBER_ID " +
                        "WHERE D.MEMBER_ID = :userId";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserId = Convert.ToInt32(reader["MEMBER_ID"]),
                            Username = reader["USER_NAME"].ToString()
                        };

                        Demand demand = new Demand
                        {
                            DemandId = Convert.ToInt32(reader["DEMAND_ID"]),
                            User = user,
                            BookName = reader["BOOK_NAME"].ToString(),
                            Writer = reader["WRITER"].ToString(),
                            RequestedDate = reader["REQUESTED_DATE"].ToString()
                        };

                        demands.Add(demand);
                    }

                }
            }

            return demands;
        }

        public Demand GetDemandByUserId(int userId) //not used yet
        {
            throw new Exception();
        }
    }
}
