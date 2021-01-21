using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly string _connectionString;

        public CategoryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }


        public IEnumerable<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Select * From CATEGORIES";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = new Category
                        {
                            CategoryId = Convert.ToInt32(reader["CATEGORY_ID"]),
                            CategoryName = reader["CATEGORY_NAME"].ToString()
                        };

                        categories.Add(category);
                    }

                }
            }
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            Category category = new Category();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Select * From CATEGORIES Where CATEGORY_ID = :id";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        category = new Category
                        {
                            CategoryId = Convert.ToInt32(reader["CATEGORY_ID"]),
                            CategoryName = reader["CATEGORY_NAME"].ToString()
                        };


                    }

                }
            }
            return category;
        }

        public Category AddCategory(Category category)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("categoryName", category.CategoryName);
                    command.CommandText = "INSERT INTO CATEGORIES(CATEGORY_NAME) VALUES( :categoryName)";
                    command.ExecuteNonQuery();

                }
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {

            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {

                        connection.Open();

                        command.BindByName = true;

                        command.Parameters.Add("id", category.CategoryId);
                        command.Parameters.Add("name", category.CategoryName);
                        command.CommandText = "Update CATEGORIES Set CATEGORY_NAME = :name  Where CATEGORY_ID = :id";
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch
            {
                throw;
            }
            return category;
        }

        public int DeleteCategory(int id) 
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Delete From CATEGORIES Where CATEGORY_ID = :id";
                    command.ExecuteNonQuery();

                }
            }
            return id;
        }
    }
}
