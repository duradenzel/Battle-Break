using System;
using System.Collections.Generic;
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
                    gameDTO.Name = reader.GetString("name");
                    gameDTO.Minimum_Players = reader.GetInt32("minimum_players");
                    gameDTO.Rules = reader.GetString("rules");
                    gameDTO.Win_Condition = reader.GetString("win_condition");
                }
                con.Close();
            }

            return gameDTO;
        }
    }
}
