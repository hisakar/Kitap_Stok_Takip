using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly string _connectionString;
        public PublisherService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }
        public IEnumerable<Publisher> GetAllPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Select * From PUBLISHERS";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Publisher publisher = new Publisher
                        {
                            PublisherId = Convert.ToInt32(reader["PUBLISHER_ID"]),
                            PublisherName = reader["PUBLISHER_NAME"].ToString()
                        };

                        publishers.Add(publisher);
                    }

                }
            }
            return publishers;
        }

        public Publisher GetPublisherById(int id)
        {
            Publisher publisher = new Publisher();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Select * From PUBLISHERS Where PUBLISHER_ID = :id";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        publisher = new Publisher
                        {
                            PublisherId = Convert.ToInt32(reader["PUBLISHER_ID"]),
                            PublisherName = reader["PUBLISHER_NAME"].ToString()
                        };
                    }

                }
            }
            return publisher;
        }

        public Publisher AddPublisher(Publisher publisher)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("publisherName", publisher.PublisherName);
                    command.CommandText = "INSERT INTO PUBLISHERS (PUBLISHER_NAME) VALUES( :publisherName)";
                    command.ExecuteNonQuery();

                }
            }
            return publisher;
        }
        public int DeletePublisher(int id)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Delete From PUBLISHERS Where PUBLISHER_ID = :id";
                    command.ExecuteNonQuery();

                }
            }
            return id;
        }

        
    }
}
