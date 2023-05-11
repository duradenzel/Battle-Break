using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BattleBreakDAL.DTOS;
using MySql.Data.MySqlClient;

namespace BattleBreakDAL
{
    public class MatchDAL
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        public MatchDAL() { }

        public List<MatchDTO> GetMatchWithID(int ID)
        {
            List<MatchDTO> matchList = new();

            using (MySqlConnection con = new(_connString))
            {
                con.Open();
                MySqlCommand command = new($"SELECT * FROM `match` WHERE `ID` = {ID} ", con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MatchDTO match = new()
                    {
                        Match_ID = reader.GetInt32("ID"),
                        Game_ID = reader.GetInt32("game_ID"),
                        Account_ID = reader.GetInt32("account_ID"),
                        Won = reader.GetInt32("won"),
                        Points = reader.GetInt32("points"),
                    };

                    matchList.Add(match);
                }
                con.Close();
            }

            return matchList;
        }

        public List<MatchDTO> GetMatches()
        {
            List<MatchDTO> matchList = new();


            using (MySqlConnection con = new(_connString))
            {
                con.Open();
                MySqlCommand command = new($"SELECT * FROM `match`", con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MatchDTO match = new()
                    {
                        Match_ID = reader.GetInt32("ID"),
                        Game_ID = reader.GetInt32("game_ID"),
                        Account_ID = reader.GetInt32("account_ID"),
                        Won = reader.GetInt32("won"),
                        Points = reader.GetInt32("points"),
                    };

                    matchList.Add(match);
                }
                con.Close();
            }

            return matchList;
        }

        public List<AccountDTO> GetAccounts(int ID)
        {
            List<AccountDTO> accountList = new();

            using(MySqlConnection con = new(_connString))
            {
                con.Open();
                MySqlCommand command = new($"SELECT * FROM `account` WHERE `ID` IN (SELECT `account_ID` FROM `match` WHERE `ID` = {ID})", con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AccountDTO account = new()
                    {
                        Account_ID = reader.GetInt32("ID"),
                        User_Name = reader.GetString("username"),
                        Full_Name = reader.GetString("full_name"),
                        Email = reader.GetString("email"),
                        Password = reader.GetString("password"),
                        Type = reader.GetString("type"),
                    };
                    accountList.Add(account);
                }
                con.Close();
            }

            return accountList;
        }

        public int GetGreatestMatchID()
        {
            int ID = 0;

            using (MySqlConnection con = new(_connString))
            {
                MySqlDataReader reader;

                con.Open();
                MySqlCommand getIdCom = new("SELECT `ID` FROM `match` ORDER BY `ID` ASC", con);
                reader = getIdCom.ExecuteReader();

                while (reader.Read())
                {
                    ID = reader.GetInt32("ID");
                }
                con.Close();
            }

            return ID;
        }

        public void CreateMatch(int ID, int Game_ID, int User_ID, int Won, int Score)
        {
            using(MySqlConnection con = new(_connString))
            {
                MySqlDataReader reader;
                con.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.CommandText = $"INSERT INTO `match` (ID, game_ID, account_ID, won, points) VALUES ({ID},1,{User_ID},{Won},{Score})";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    reader = cmd.ExecuteReader();
                }
                con.Close();
            }
        }
    }
}
