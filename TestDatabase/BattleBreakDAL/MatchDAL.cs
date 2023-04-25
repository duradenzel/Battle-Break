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

        public List<MatchDTO> GetMatches(int ID)
        {
            List<MatchDTO> matchList = new();


            using (MySqlConnection con = new(_connString))
            {
                con.Open();
                MySqlCommand command = new($"Select * from `wedstrijd` where `ID` = {ID} ", con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MatchDTO match = new()
                    {
                        Match_ID = reader.GetInt32(0),
                        Game_ID = reader.GetInt32(1),
                        Account_ID = reader.GetInt32(2),
                        Won = reader.GetInt32(3),
                        Points = reader.GetInt32(4),
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
                MySqlCommand command = new($"Select * From `Account` Where `ID` IN (Select `Account_ID` From `Wedstrijd` where `ID` = {ID})", con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AccountDTO account = new()
                    {
                        Account_ID = reader.GetInt32(0),
                        User_Name = reader.GetString(1),
                        Full_Name = reader.GetString(2),
                        Email = reader.GetString(3),
                        Password = reader.GetString(4),
                        Type = reader.GetString(5),
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
                MySqlCommand getIdCom = new("Select `ID` from `wedstrijd` ORDER BY `ID` ASC", con);
                reader = getIdCom.ExecuteReader();

                while (reader.Read())
                {
                    ID = reader.GetInt32(0);
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
                    cmd.CommandText = $"INSERT INTO wedstrijd (ID, Spel_ID, Account_ID, Gewonnen, Punten) VALUES ({ID},1,{User_ID},{Won},{Score})";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    reader = cmd.ExecuteReader();
                }
                con.Close();
            }
        }
    }
}
