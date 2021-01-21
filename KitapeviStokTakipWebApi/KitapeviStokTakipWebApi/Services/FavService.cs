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
    public class FavService : IFavService
    {
        private readonly string _connectionString;

        public FavService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        public bool AddFav(Fav fav)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = new OracleCommand("SP_INSERT_FAV"))
                {
                    connection.Open();
                    using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        OracleParameter[] prm = new OracleParameter[2];

                        prm[0] = command.Parameters.Add("v_MEMBER_ID", OracleDbType.Int32,
                           fav.User.UserId, ParameterDirection.Input);
                        prm[1] = command.Parameters.Add("v_BOOK_ID", OracleDbType.Int32,
                           fav.Book.BookId, ParameterDirection.Input);

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

        public void DeleteFavsByUserId(int userId) //not used yet
        {
            throw new NotImplementedException();
        }

        public void DeleteSingleFav(Fav fav)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("userId", fav.User.UserId);
                    command.Parameters.Add("bookId", fav.Book.BookId);
                    command.CommandText = "Delete From FAVORITES Where BOOK_ID = :bookId AND MEMBER_ID = :userId";
                    command.ExecuteNonQuery();

                }
            }

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
                        "FROM FAVORITES FAV JOIN BOOKS B ON FAV.BOOK_ID = B.BOOK_ID JOIN PUBLISHERS P ON P.PUBLISHER_ID = B.PUBLISHER_ID JOIN CATEGORIES C ON C.CATEGORY_ID = B.CATEGORY_ID " +
                        "WHERE FAV.MEMBER_ID = :userId";

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
    
        public IEnumerable<Fav> GetAllFavs() //not used yet
        {
            throw new NotImplementedException();
        }

        public Fav GetFavByUserId(int id) //not used yet
        {
            throw new NotImplementedException();
        }
    }
}
