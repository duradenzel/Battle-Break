using MySql.Data.MySqlClient;
using System.Diagnostics;
using BattleBreakDAL.DTOS;



namespace BattleBreakDAL
{
    public class AuthDAL
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
        public AccountDTO? GetById(int ID)
        {
            using var connection = new MySqlConnection(_connString);
            connection.Open();

            var sql = "SELECT * FROM account WHERE ID = @ID";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ID", ID);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new AccountDTO
                {
                    ID = reader.GetInt32("ID"),
                    username = reader.GetString("username"),
                    email = reader.GetString("email"),
                    password = reader.GetString("password"),
                    type = reader.GetString("type"),
                    image_url = reader.GetString("image")
                };          
            }
            return null;
        }

        public async Task<AccountDTO?> GetAccountByEmailAsync(string email)
        {
            using var connection = new MySqlConnection(_connString);
            await connection.OpenAsync();

            var sql = "SELECT * FROM account WHERE email = @email";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@email", email);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new AccountDTO
                {
                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                    username = reader.GetString(reader.GetOrdinal("username")),
                    email = reader.GetString(reader.GetOrdinal("email")),
                    password = reader.GetString(reader.GetOrdinal("password")),
                    type = reader.GetString(reader.GetOrdinal("type")),
                    image_url = reader.GetString(reader.GetOrdinal("image"))
                };
            }
            return null;
        }

        public async Task<AccountDTO?> GetAccountByUsernameAsync(string username)
        {
            using var connection = new MySqlConnection(_connString);
            await connection.OpenAsync();

            var sql = "SELECT * FROM account WHERE username = @username";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new AccountDTO
                {
                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                    username = reader.GetString(reader.GetOrdinal("username")),
                    email = reader.GetString(reader.GetOrdinal("email")),
                    password = reader.GetString(reader.GetOrdinal("password")),
                    type = reader.GetString(reader.GetOrdinal("type")),
                    image_url = reader.GetString(reader.GetOrdinal("image"))
                };
            }
            return null;
        }

        public async Task AddAccountAsync(AccountDTO account)
        {
            using var connection = new MySqlConnection(_connString);
            await connection.OpenAsync();

            var sql = "INSERT INTO account (username, email, password) VALUES (@username, @email, @password)";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@username", account.username);
            cmd.Parameters.AddWithValue("@email", account.email);
            cmd.Parameters.AddWithValue("@password", account.password);

            await cmd.ExecuteNonQueryAsync();

            account.ID = (int)cmd.LastInsertedId;
        }










        //public (bool, string) Authenticate(string email, string password)
        //{
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(_connString))
        //        {
        //            string query = "SELECT `password`, `type` FROM `account` WHERE `email` = @email";

        //            using (MySqlCommand command = new MySqlCommand(query, conn))
        //            {
        //                command.Parameters.AddWithValue("@email", email);
        //                conn.Open();

        //                using (MySqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {

        //                        string passwordHash = reader.GetString("password");

        //                        if (BCrypt.Net.BCrypt.Verify(password, passwordHash))
        //                        {
        //                            string type = reader.GetString("type");
        //                            return (true, type);
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

        //    return (false, null);
        //}



        //public bool Register(string username, string full_name, string email, string password)
        //{
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(_connString))
        //        {
        //            string salt = BCrypt.Net.BCrypt.GenerateSalt();
        //            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        //            string query = "INSERT INTO `account` (username, full_name, email, password) VALUES (@username, @full_name, @email, @password)";

        //            using (MySqlCommand command = new MySqlCommand(query, conn))
        //            {
        //                command.Parameters.AddWithValue("@username", username);
        //                command.Parameters.AddWithValue("@full_name", full_name);
        //                command.Parameters.AddWithValue("@email", email);
        //                command.Parameters.AddWithValue("@password", passwordHash);
        //                conn.Open();
        //                int result = command.ExecuteNonQuery();
        //                if (result > 0)
        //                {
        //                    Debug.Write("Record inserted successfully");
        //                    return true;
        //                }
        //                else
        //                {
        //                    Debug.Write("Error inserting record");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Write("Error during registration: " + ex.Message);
        //    }

        //    return false;
        //}

        //public string GetUserType(string email)
        //{
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(_connString))
        //        {
        //            string query = "SELECT `type` FROM `account` WHERE email = @email";

        //            using (MySqlCommand command = new MySqlCommand(query, conn))
        //            {
        //                command.Parameters.AddWithValue("@email", email);
        //                conn.Open();

        //                object result = command.ExecuteScalar();

        //                if (result != null)
        //                {
        //                    return result.ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Write("Error retrieving user type: " + ex.Message);
        //    }

        //    return null;
        //}


        //public bool MakeAdmin()
        //{


        //    return true;
        //}

    }
}
