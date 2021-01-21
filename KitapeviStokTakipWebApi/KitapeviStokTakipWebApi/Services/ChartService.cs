using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Services
{
    public class ChartService : IChartService
    {
        private readonly string _connectionString;

        public ChartService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        public IEnumerable<Chart> GetAllChartItems()
        {

            List<Chart> charts = new List<Chart>();

            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {

                        connection.Open();

                        command.BindByName = true;
                        command.CommandText = "SELECT B.*, P.*, C.*, M.*, O.ORDERED_DATE  " +
                            "FROM ORDERS O JOIN BOOKS B ON O.BOOK_ID = B.BOOK_ID " +
                            "JOIN MEMBERS M ON O.MEMBER_ID = M.MEMBER_ID " +
                            "JOIN PUBLISHERS P ON P.PUBLISHER_ID = B.PUBLISHER_ID " +
                            "JOIN CATEGORIES C ON C.CATEGORY_ID = B.CATEGORY_ID";
                    
                        OracleDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Category category = new Category
                            {
                                CategoryId = Convert.ToInt32(reader["CATEGORY_ID"]),
                                CategoryName = reader["CATEGORY_NAME"].ToString()
                            };

                            Publisher publisher = new Publisher
                            {
                                PublisherId = Convert.ToInt32(reader["PUBLISHER_ID"]),
                                PublisherName = reader["PUBLISHER_NAME"].ToString()
                            };

                            Book book = new Book
                            {
                                BookId = Convert.ToInt32(reader["BOOK_ID"]),
                                BookName = reader["BOOK_NAME"].ToString(),
                                Writer = reader["WRITER"].ToString(),
                                Price = Convert.ToInt32(reader["PRICE"]),
                                StockAmount = Convert.ToInt32(reader["STOCK_AMOUNT"]),
                                Barcode = reader["BARCODE"].ToString(),
                                Category = category,
                                Publisher = publisher,
                                AddedDate = reader["ADDED_DATE"].ToString()
                            };
                            User user = new User
                            {
                                UserId = Convert.ToInt32(reader["MEMBER_ID"]),
                                Username = reader["USER_NAME"].ToString()
                            };

                            Chart chart = new Chart
                            {
                               Book = book,
                               User = user,
                               OrderedDate = reader["ORDERED_DATE"].ToString()
                            };
                            charts.Add(chart);
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return charts;


        }

        public bool AddChartItem(Chart chartItem)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = new OracleCommand("SP_INSERT_ORDER"))
                {
                    connection.Open();
                    using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        OracleParameter[] prm = new OracleParameter[2];

                        prm[0] = command.Parameters.Add("v_MEMBER_ID", OracleDbType.Int32,
                           chartItem.User.UserId, ParameterDirection.Input);
                        prm[1] = command.Parameters.Add("v_BOOK_ID", OracleDbType.Int32,
                           chartItem.Book.BookId, ParameterDirection.Input);

                        command.Connection = connection;
                        try
                        {
                            command.ExecuteNonQuery();
                            connection.Close();
                            return true;
                        }
                        catch
                        {
                            connection.Close();
                            return false;
                        };
                    }
                }
            }
        }

        public void DeleteChartItemByUserId(int userId) //not used yet
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooksByUserId(int userId)
        {
            List<Book> books = new List<Book>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;

                    command.Parameters.Add("userId", userId);
                    command.CommandText = "SELECT B.*, P.*, C.* " +
                        "FROM ORDERS O JOIN BOOKS B ON O.BOOK_ID = B.BOOK_ID JOIN PUBLISHERS P ON P.PUBLISHER_ID = B.PUBLISHER_ID JOIN CATEGORIES C ON C.CATEGORY_ID = B.CATEGORY_ID " +
                        "WHERE O.MEMBER_ID = :userId";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = new Category
                        {
                            CategoryId = Convert.ToInt32(reader["CATEGORY_ID"]),
                            CategoryName = reader["CATEGORY_NAME"].ToString()
                        };

                        Publisher publisher = new Publisher();
                        try
                        {
                            publisher = new Publisher
                            {
                                PublisherId = Convert.ToInt32(reader["PUBLISHER_ID"]),
                                PublisherName = reader["PUBLISHER_NAME"].ToString()
                            };
                        }
                        catch
                        {
                            Console.WriteLine("err from oublisher");
                        }

                        Book book = new Book
                        {
                            BookId = Convert.ToInt32(reader["BOOK_ID"]),
                            BookName = reader["BOOK_NAME"].ToString(),
                            Writer = reader["WRITER"].ToString(),
                            Price = Convert.ToInt32(reader["PRICE"]),
                            StockAmount = Convert.ToInt32(reader["STOCK_AMOUNT"]),
                            Barcode = reader["BARCODE"].ToString(),
                            Category = category,
                            Publisher = publisher,
                            AddedDate = reader["ADDED_DATE"].ToString()
                        };

                        books.Add(book);
                    }

                }
            }

            return books;
        }

        public void DeleteSingleChartItem(Chart chartItem)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("userId", chartItem.User.UserId);
                    command.Parameters.Add("bookId", chartItem.Book.BookId);
                    command.CommandText = "DELETE FROM ORDERS WHERE BOOK_ID = :bookId AND MEMBER_ID = :userId";
                    command.ExecuteNonQuery();

                }
            }

        }

        public Chart GetChartItemByUserId(int id) //not used yet
        {
            throw new NotImplementedException();
        }
    }
}
