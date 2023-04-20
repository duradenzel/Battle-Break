using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL
{
    public class UserDAO
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        public UserDAO()
        {

        }

        //public bool Authenticate(string email, string password)
        //{
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(_connString))
        //        {
        //            string query = "SELECT Wachtwoord FROM account WHERE Email = @Email";

        //            using (MySqlCommand command = new MySqlCommand(query, conn))
        //            {
        //                command.Parameters.AddWithValue("@Email", email);
        //                conn.Open();

        //                using (MySqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        string passwordHash = reader.GetString("Wachtwoord");

        //                        if (BCrypt.Net.BCrypt.Verify(password, passwordHash))
        //                        {

        //                            return true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Write("Error during authentication: " + ex.Message);
        //    }

        //    return false;
        //}

        public (bool, string) DALAuthenticate(string email, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connString))
                {
                    string query = "SELECT Wachtwoord, Type FROM account WHERE Email = @Email";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        conn.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string passwordHash = reader.GetString("Wachtwoord");

                                if (BCrypt.Net.BCrypt.Verify(password, passwordHash))
                                {
                                    string type = reader.GetString("Type");
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

        public bool DALRegister(string username, string fullname, string email, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connString))
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

                    string query = "INSERT INTO account (Gebruikersnaam, VolledigeNaam, Email, Wachtwoord) VALUES (@Gebruikersnaam, @VolledigeNaam, @Email, @Wachtwoord)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Gebruikersnaam", username);
                        command.Parameters.AddWithValue("@VolledigeNaam", fullname);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Wachtwoord", passwordHash);
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
    }
}
