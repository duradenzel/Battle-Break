using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            List<GameDTO> gameList = new List<GameDTO>();

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
                        name = reader.GetString("name"),
                        minimum_players = reader.GetInt32("minimum_players"),
                        rules = reader.GetString("rules"),
                        win_condition = reader.GetString("win_condition"),
                    };

                    gameList.Add(game);
                }
                con.Close();
            }

            return gameList;
        }

        public GameDTO GetGameWithID(int ID)
        {
            GameDTO gameDTO = new();

            using (MySqlConnection con = new(_connString))
            {
                con.Open();
                MySqlCommand command = new($"SELECT * FROM `game` where `ID` = {ID}", con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    gameDTO.ID = reader.GetInt32("ID");
                    gameDTO.name = reader.GetString("name");
                    gameDTO.minimum_players = reader.GetInt32("minimum_players");
                    gameDTO.rules = reader.GetString("rules");
                    gameDTO.win_condition = reader.GetString("win_condition");
                }
                con.Close();
            }

            return gameDTO;
        }

        public void GameAddD(int ID, string name, int minimum_players, string rules, string win_condition)
        {
            // return the create view
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                string query = "INSERT INTO game (name, minimum_players, rules, win_condition) VALUES (@name, @minimum_players, @rules, @win_condition)";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@name", name); // update parameter name to @naam
                    cmd.Parameters.AddWithValue("@minimum_players", minimum_players);
                    cmd.Parameters.AddWithValue("@rules", rules);
                    cmd.Parameters.AddWithValue("@win_condition", win_condition);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GameChangeD(int ID, string name, int minimum_players, string rules, string win_condition)
        {

            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                string query = "UPDATE game SET name = @name, minimum_players = @minimum_players, rules = @rules, win_condition = @win_condition WHERE ID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@minimum_players", minimum_players); // update parameter name to @naam
                    cmd.Parameters.AddWithValue("@rules", rules);
                    cmd.Parameters.AddWithValue("@win_condition", win_condition);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public void DeleteGameD(int ID)
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                connection.Open();
                string query = "DELETE FROM game WHERE ID = @ID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }
            }
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
                                account.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                                account.username = reader.GetString(reader.GetOrdinal("username"));
                                account.full_name = reader.GetString(reader.GetOrdinal("full_name"));
                                account.email = reader.GetString(reader.GetOrdinal("email"));

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

