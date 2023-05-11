using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BattleBreakDAL
{
    public class AuthDAL
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        public AuthDAL() { }

        public (bool, string) Authenticate(string email, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connString))
                {
                    string query = "SELECT `password`, `type` FROM `account` WHERE `email` = @email";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        conn.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                string passwordHash = reader.GetString("password");

                                if (BCrypt.Net.BCrypt.Verify(password, passwordHash))
                                {
                                    string type = reader.GetString("type");
                                    return (true, type);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error during authentication: " + ex.Message);
            }

            return (false, null);
        }

        public bool Register(string username, string full_name, string email, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connString))
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

                    string query = "INSERT INTO `account` (username, full_name, email, password) VALUES (@username, @full_name, @email, @password)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@full_name", full_name);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", passwordHash);
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Debug.Write("Record inserted successfully");
                            return true;
                        }
                        else
                        {
                            Debug.Write("Error inserting record");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error during registration: " + ex.Message);
            }

            return false;
        }

        public string GetUserType(string email)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connString))
                {
                    string query = "SELECT `type` FROM `account` WHERE email = @email";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        conn.Open();

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error retrieving user type: " + ex.Message);
            }

            return null;
        }


        public bool MakeAdmin()
        {


            return true;
        }

    }
}
