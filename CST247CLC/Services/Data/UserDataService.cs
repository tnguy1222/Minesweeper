using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CST247CLC.Models;

namespace CST247CLC.Services.Data
{
    public class UserDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CST247CLC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool authenticate(UserModel user)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.USERS WHERE USERNAME = @USERNAME and PASSWORD = @PASSWORD";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("USERNAME", System.Data.SqlDbType.VarChar, 40).Value = user.userName;
                command.Parameters.Add("PASSWORD", System.Data.SqlDbType.VarChar, 40).Value = user.password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return success;
        }

        public bool createUser(UserModel user)
        {
            bool success = false;

            string sqlStatement = "INSERT INTO dbo.users (USERNAME, PASSWORD, FIRST_NAME, LAST_NAME, SEX, AGE, EMAIL, STATE) VALUES (@USERNAME, @PASSWORD, @FIRST_NAME, @LAST_NAME, @SEX, @AGE, @EMAIL, @STATE)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 40).Value = user.userName;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 40).Value = user.password;
                command.Parameters.Add("@FIRST_NAME", System.Data.SqlDbType.VarChar, 40).Value = user.firstName;
                command.Parameters.Add("@LAST_NAME", System.Data.SqlDbType.VarChar, 40).Value = user.lastName;
                command.Parameters.Add("@SEX", System.Data.SqlDbType.Int).Value = user.sex;
                command.Parameters.Add("@AGE", System.Data.SqlDbType.Int).Value = user.age;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 40).Value = user.email;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 40).Value = user.state;

                try
                {
                    connection.Open();

                    //check if there was a row affected
                    if (command.ExecuteNonQuery() > 0)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return success;
        }
    }
}
