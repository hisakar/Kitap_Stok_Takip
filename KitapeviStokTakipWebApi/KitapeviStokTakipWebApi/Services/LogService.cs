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
    public class LogService : ILogService
    {
        private readonly string _connectionString;

        public LogService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        public IEnumerable<CategoryLog> GetAllCategoryLogs()
        {
            List<CategoryLog> categoryLogs = new List<CategoryLog>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Select * From CATEGORY_LOG";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CategoryLog categoryLog = new CategoryLog
                        {
                            LogId = Convert.ToInt32(reader["LOG_ID"]),
                            LogType = reader["LOG_TYPE"].ToString(),
                            NewName = reader["NEW_NAME"].ToString(),
                            OldName = reader["OLD_NAME"].ToString(),
                            LogDate = reader["LOG_DATE"].ToString(),
                        };

                        categoryLogs.Add(categoryLog);
                    }

                }
            }
            return categoryLogs;
        }

        public CategoryLog GetCategoryLogById(int id) //not used yet
        {
            throw new NotImplementedException();
        }

        public int DeleteCategoryLogById(int id)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Delete From CATEGORY_LOG Where LOG_ID = :id";
                    command.ExecuteNonQuery();

                }
            }
            return id;
        }

        public void DeleteAllCategoryLogs()
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Delete From CATEGORY_LOG";
                    command.ExecuteNonQuery();

                }
            }
        }

        public IEnumerable<BookLog> GetAllBookLogs()
        {
            List<BookLog> bookLogs = new List<BookLog>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Select * From BOOK_LOG";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        BookLog bookLog = new BookLog
                        {
                            LogId = Convert.ToInt32(reader["LOG_ID"]),
                            LogType = reader["LOG_TYPE"].ToString(),
                            NewName = reader["NEW_NAME"].ToString(),
                            NewWriter = reader["NEW_WRITER"].ToString(),
                            NewPrice = reader["NEW_PRICE"].ToString(),
                            NewStockAmount = reader["NEW_STOCK_AMOUNT"].ToString(),
                            NewBarcode = reader["NEW_BARCODE"].ToString(),
                            NewCategoryId = reader["NEW_CATEGORY_ID"].ToString(),
                            OldName = reader["OLD_NAME"].ToString(),
                            OldWriter = reader["OLD_WRITER"].ToString(),
                            OldPrice = reader["OLD_PRICE"].ToString(),
                            OldStockAmount = reader["OLD_STOCK_AMOUNT"].ToString(),
                            OldBarcode = reader["OLD_BARCODE"].ToString(),
                            OldCategoryId = reader["OLD_CATEGORY_ID"].ToString(),
                            LogDate = reader["LOG_DATE"].ToString(),
                        };

                        bookLogs.Add(bookLog);
                    }

                }
            }
            return bookLogs;
        }

        public BookLog GetBookLogById(int id) //not used yet
        {
            throw new NotImplementedException();
        }

        public int DeleteBookLogById(int id)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Delete From BOOK_LOG Where LOG_ID = :id";
                    command.ExecuteNonQuery();

                }
            }
            return id;
        }


        public void DeleteAllBookLogs()
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Delete From BOOK_LOG";
                    command.ExecuteNonQuery();

                }
            }
        }
    }
}
