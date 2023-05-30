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
        public class ListMetAccounts
        {
            public List<AccountDTO> AllAccountsD(int ID, string user_name, string email, string password, string type)
            {
                string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

                List<BattleBreakDAL.DTOS.AccountDTO> accounts = new List<BattleBreakDAL.DTOS.AccountDTO>();
                using (MySqlConnection con = new MySqlConnection(connString))
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
                                BattleBreakDAL.DTOS.AccountDTO account = new BattleBreakDAL.DTOS.AccountDTO();
                                account.ID = Convert.ToInt32(reader["ID"]);
                                account.username = Convert.ToString(reader["user_name"]);
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

        }
    }
}
