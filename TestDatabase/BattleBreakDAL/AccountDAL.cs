using BattleBreakDAL.DTOS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL
{
    public class AccountDAL
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        public AccountDTO GetAccountWithID(int ID)
        {
            AccountDTO account = new();

            using (MySqlConnection con = new(_connString))
            {
                con.Open();
                MySqlCommand command = new($"SELECT * FROM `account` WHERE `ID` = {ID} ", con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    account = new()
                    {
                        ID = reader.GetInt32("ID"),
                        username = reader.GetString("username"),
                        full_name = reader.GetString("full_name"),
                        email = reader.GetString("email"),
                        password = reader.GetString("password"),
                        type = reader.GetString("type"),
                    };


                }
                con.Close();
            }

            return account;
        }
        public async Task<AccountDTO> GetAccountWithIDAsync(int ID)
        {
            AccountDTO accountDTO = await Task.Run(() =>
            {
                AccountDTO account = new AccountDTO();

                using (MySqlConnection con = new MySqlConnection(_connString))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand($"SELECT * FROM `account` WHERE `ID` = {ID} ", con);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        account = new()
                        {
                            ID = reader.GetInt32("ID"),
                            username = reader.GetString("username"),
                            full_name = reader.GetString("full_name"),
                            email = reader.GetString("email"),
                            password = reader.GetString("password"),
                            type = reader.GetString("type"),
                        };
                    }

                    con.Close();
                }

                return account;
            });

            AccountDTO accountModel = new AccountDTO
            {
                ID = accountDTO.ID,
                username = accountDTO.username,
                full_name = accountDTO.full_name,
                password = accountDTO.password,
                email = accountDTO.email,
                type = accountDTO.type
            };

            return accountModel;
        }


        public List<AccountDTO> AllAccountsD()
        {
            List<AccountDTO> accounts = new List<AccountDTO>();
            using (MySqlConnection con = new MySqlConnection(_connString))
            {
                con.Open();

                // Query to retrieve data from the database
                string query = "SELECT * FROM account"; // Replace 'YourTableName' with the actual name of your database table
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a new Account object and populate its properties from the retrieved data
                            AccountDTO account = new AccountDTO();
                            account.ID = Convert.ToInt32(reader["ID"]);
                            account.username = Convert.ToString(reader["username"]);
                            account.full_name = Convert.ToString(reader["full_name"]);
                            account.email = Convert.ToString(reader["email"]);
                            account.password = Convert.ToString(reader["password"]);

                            account.type = Convert.ToString(reader["type"]);

                            // Add the populated Account object to the list
                            accounts.Add(account);
                        }
                    }
                }
            }
            return accounts;
        }

        public void MakeUserD(int id)
        {
            List<AccountDTO> accounts = new List<AccountDTO>();
            using (MySqlConnection con = new MySqlConnection(_connString))
            {
                con.Open();
                string query = "SELECT * FROM account WHERE ID = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve the current type of the account
                            string currentType = reader.GetString("Type");

                            // Modify the type of the account
                            // You can update the type based on your business logic
                            string newType = "Gebruiker";
                            // You can also retrieve the new type from user input or other sources

                            // Close the data reader before executing another query
                            reader.Close();

                            // Update the type of the account in the database
                            string updateQuery = "UPDATE Account SET Type = @newType WHERE ID = @id";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, con))
                            {
                                updateCmd.Parameters.AddWithValue("@newType", newType);
                                updateCmd.Parameters.AddWithValue("@id", id);
                                updateCmd.ExecuteNonQuery();
                            }

                            // Output a message to indicate the type change
                            Console.WriteLine($"Account with ID {id} has its type changed from {currentType} to {newType}.");
                        }
                        else
                        {
                            // Account with the given ID not found
                            Console.WriteLine($"Account with ID {id} not found.");
                        }
                    }
                }
            }
        }

        public void MakeAdminD(int id)
        {
            List<AccountDTO> accounts = new List<AccountDTO>();
            using (MySqlConnection con = new MySqlConnection(_connString))
            {
                con.Open();
                string query = "SELECT * FROM account WHERE ID = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve the current type of the account
                            string currentType = reader.GetString("Type");

                            // Modify the type of the account
                            // You can update the type based on your business logic
                            string newType = "Admin";
                            // You can also retrieve the new type from user input or other sources

                            // Close the data reader before executing another query
                            reader.Close();

                            // Update the type of the account in the database
                            string updateQuery = "UPDATE Account SET Type = @newType WHERE ID = @id";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, con))
                            {
                                updateCmd.Parameters.AddWithValue("@newType", newType);
                                updateCmd.Parameters.AddWithValue("@id", id);
                                updateCmd.ExecuteNonQuery();
                            }

                            // Output a message to indicate the type change
                            Console.WriteLine($"Account with ID {id} has its type changed from {currentType} to {newType}.");
                        }
                        else
                        {
                            // Account with the given ID not found
                            Console.WriteLine($"Account with ID {id} not found.");
                        }
                    }
                }
            }
        }
    }
}
