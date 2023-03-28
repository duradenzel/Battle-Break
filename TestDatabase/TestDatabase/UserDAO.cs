using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TestDatabase
{
    public class UserDAO
    {
        private readonly string _connString;

        public UserDAO(string connString)
        {
            _connString = connString;
        }

        public bool Authenticate(string email, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connString))
                {
                    string query = "SELECT Wachtwoord FROM account WHERE Email = @Email";

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
                                    return true;
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

            return false;
        }

        public bool Register(string username, string fullname, string email, string password)
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

