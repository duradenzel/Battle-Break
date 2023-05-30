using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBreakDAL.DTOS;
using MySql.Data.MySqlClient;

namespace BattleBreakDAL
{
    public class GameDAL
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        public GameDAL() { }

        public List<GameDTO> GetGames()
        {
            List<GameDTO > gameList = new List<GameDTO>();

            using (MySqlConnection con = new(_connString))
            {
                con.Open();
                MySqlCommand command = new($"SELECT * FROM `game`", con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GameDTO game = new()
                    {
                        ID = reader.GetInt32("ID"),
                        Name = reader.GetString("name"),
                        Minimum_Players = reader.GetInt32("minimum_players"),
                        Rules = reader.GetString("rules"),
                        Win_Condition = reader.GetString("win_condition"),
                    };

                    gameList.Add(game);
                }
                con.Close();
            }

            return gameList;
        }


        public async Task<List<AccountDTO>> GetAccounts()
        {
            List<AccountDTO> accounts = new();
            try
            {
                using (MySqlConnection conn = new(_connString))
                {
                    string query = "SELECT * FROM account";

                    conn.Open();
                    using (MySqlCommand command = new(query, conn))
                    {
                       

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                AccountDTO account = new();
                                account.Account_ID = reader.GetInt32(reader.GetOrdinal("ID"));
                                account.User_Name = reader.GetString(reader.GetOrdinal("username"));
                                account.Full_Name = reader.GetString(reader.GetOrdinal("full_name"));
                                account.Email = reader.GetString(reader.GetOrdinal("email"));

                                accounts.Add(account);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error fetching accounts: " + ex.Message);
            }

            return accounts;
        }

    }
}
