using Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCR_DProject.DBContext
{
    public class DbContext
    {
        public class User
        {
            public string UD_NAME { get; set; }
            //public string ED_NAME { get; set; }
        }
        public class UserService
        {
            private readonly string _connectionString;

            public UserService()
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }

            public List<User> GetUserNames(string searchTerm)
            {
                var users = new List<User>();

                // Define the SQL query with a parameter placeholder
                //string query = "SELECT ED_NAME FROM EMPLOYEEDATA WHERE ED_NAME LIKE @SearchTerm";
                string query = "SELECT TOP 1000 UD_NAME FROM USERDATA WHERE UD_NAME LIKE @SearchTerm";

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        using (var command = new SqlCommand(query, connection))
                        {
                            // Use parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%"); 

                            connection.Open();
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    users.Add(new User
                                    {
                                        UD_NAME = reader["UD_NAME"].ToString()
                                    });
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("An error occurred while executing the query: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }

                return users;
            }

        }
    }
}