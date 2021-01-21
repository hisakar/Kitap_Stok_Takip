using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace KitapeviStokTakipWebApi.Services
{
    public class BookService : IBookService
    {
        private readonly string _connectionString;

        public BookService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        public IEnumerable<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {

                        connection.Open();

                        command.BindByName = true;
                        command.CommandText = "SELECT B.*, P.*, C.* FROM BOOKS B " +
                            "JOIN PUBLISHERS P ON P.publisher_id = B.publisher_id " +
                            "JOIN CATEGORIES C ON C.category_id = B.category_id";

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

                            books.Add(book);
                        }

                    }
                }
            }
            catch
            {
                Console.WriteLine("err from getBook");
            }
            return books;
        }

        public Book GetBookById(int id)
        {
            Book book = new Book();

            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {

                        connection.Open();

                        command.BindByName = true;
                        command.Parameters.Add("id", id);
                        command.CommandText = "SELECT B.*, P.*, C.* FROM BOOKS B " +
                            "JOIN PUBLISHERS P ON P.publisher_id = B.publisher_id " +
                            "JOIN CATEGORIES C ON C.category_id = B.category_id Where BOOK_ID = :id";

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

                            book = new Book
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
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("er from getBookById");
            }

            return book;
        }

        public Book AddBook(Book book)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        OracleParameter[] prm = new OracleParameter[7];

                        prm[0] = command.Parameters.Add("categoryId", OracleDbType.Decimal,
                           book.Category.CategoryId, ParameterDirection.Input);
                        prm[1] = command.Parameters.Add("bookName", OracleDbType.Varchar2,
                          book.BookName, ParameterDirection.Input);
                        prm[2] = command.Parameters.Add("writer", OracleDbType.Varchar2,
                          book.Writer, ParameterDirection.Input);
                        prm[3] = command.Parameters.Add("price", OracleDbType.Decimal,
                          book.Price, ParameterDirection.Input);
                        prm[4] = command.Parameters.Add("stockAmount", OracleDbType.Decimal,
                          book.StockAmount, ParameterDirection.Input);
                        prm[5] = command.Parameters.Add("barcode", OracleDbType.Varchar2,
                          book.Barcode, ParameterDirection.Input);
                        prm[6] = command.Parameters.Add("publisherId", OracleDbType.Varchar2,
                          book.Publisher.PublisherId, ParameterDirection.Input);


                        var sql = "INSERT INTO BOOKS(" +
                            "CATEGORY_ID," +
                            "PUBLISHER_ID," +
                            "BOOK_NAME," +
                            "WRITER," +
                            "PRICE," +
                            "STOCK_AMOUNT," +
                            "BARCODE," +
                            "ADDED_DATE) " +
                            "VALUES(:categoryId, :publisherId, :bookName, :writer, :price, :stockAmount, :barcode, TO_CHAR(sysdate,'DD/MM/YYYY HH24:MI:SS'))";

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
            return book;
        }

        public Book UpdateBook(Book book)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {



                        connection.Open();

                        command.BindByName = true;

                        command.Parameters.Add("id", book.BookId);
                        command.Parameters.Add("bookName", book.BookName);
                        command.Parameters.Add("writer", book.Writer);
                        command.Parameters.Add("price", book.Price);
                        command.Parameters.Add("stockAmount", book.StockAmount);
                        command.Parameters.Add("barcode", book.Barcode);
                        command.Parameters.Add("categoryId", book.Category.CategoryId);
                        command.Parameters.Add("publisherId", book.Publisher.PublisherId);


                        command.CommandText = "Update BOOKS Set BOOK_NAME = :bookName," +
                            " WRITER = :writer," +
                            " PRICE = :price," +
                            " STOCK_AMOUNT = :stockAmount," +
                            " BARCODE = :barcode," +
                            " CATEGORY_ID = :categoryId," +
                            " PUBLISHER_ID = :publisherId," +
                            " ADDED_DATE = TO_CHAR(sysdate,'DD/MM/YYYY HH24:MI:SS') " +
                            " Where BOOK_ID = :id";

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch
            {
                Console.WriteLine("err from updateBook");
            }
            return book;
        }

        public int DeleteBook(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {

                        connection.Open();

                        command.BindByName = true;
                        command.Parameters.Add("id", id);
                        command.CommandText = "Delete From BOOKS Where BOOK_ID = :id";
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch
            {
                Console.WriteLine("err from deleteBook");
            }

            return id;
        }
    }
}
