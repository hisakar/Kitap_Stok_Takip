using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace KitapeviStokTakipWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }


        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Select * From MEMBERS";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserId = Convert.ToInt32(reader["MEMBER_ID"]),
                            Username = reader["USER_NAME"].ToString(),
                            Email = reader["EMAIL"].ToString()
                        };

                        users.Add(user);
                    }

                }
            }
            return users;
        }

        public User GetUserById(int id)
        {
            User user = new User();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Select * From BOOKS Where BOOK_ID = :id";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            UserId = Convert.ToInt32(reader["MEMBER_ID"]),
                            Username = reader["USER_NAME"].ToString(),
                            Email = reader["EMAIL"].ToString()
                           
                        };
                    }
                }
            }

            return user;
        }
        public int DeleteUser(int id)
        {
            throw new NotImplementedException();  //not used yet
        }
    }
}
