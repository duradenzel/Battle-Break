using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBreakDAL.DTOS;
using MySql.Data.MySqlClient;

namespace BattleBreakDAL
{
    public class TemplateDAL
    {
        //properties
        private static string connString = @"Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        //constructors
        public TemplateDAL()
        {

        }

        //methods
        public static MySqlConnection MakeConnection()
        {
            return new MySqlConnection(connString);
        }

        public List<TemplateDTO> GetTemplates()
        {
            using (MySqlConnection con = MakeConnection())
            {
                con.Open();
                MySqlCommand sqlCom = new MySqlCommand("Select * From template", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();
                List<TemplateDTO> templateList = new();

                while (reader.Read())
                {
                    TemplateDTO t = new();
                    t.id = reader.GetInt32("ID");
                    t.game = reader.GetString("game");
                    t.name = reader.GetString("name");
                    t.minimumPlayers = reader.GetInt32("minimum_players");
                    t.rules = reader.GetString("rules");
                    t.winCondition = reader.GetString("win_condition");
                    templateList.Add(t);
                }
                con.Close();
                return templateList;
            }
        }

        public List<string> GetGames()
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                MySqlCommand sqlCom = new MySqlCommand("Select DISTINCT name FROM game", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();
                List<string> gameNames = new List<string>();

                while (reader.Read())
                {
                    string gameName = reader.GetString("name");
                    gameNames.Add(gameName);
                }
                con.Close();
                return gameNames;
            }
        }

        public void TemplateAddD(TemplateDTO template)
        {
            // return the create view

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                string query = "INSERT INTO template (name, game, minimum_players, rules, win_condition) VALUES (@templateName, @templateGame, @templateMinimumPlayers, @templateRules, @templateWinCondition)";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@templateGame", template.game);
                    cmd.Parameters.AddWithValue("@templateName", template.name); // update parameter name to @naam
                    cmd.Parameters.AddWithValue("@templateMinimumPlayers", template.minimumPlayers);
                    cmd.Parameters.AddWithValue("@templateRules", template.rules);
                    cmd.Parameters.AddWithValue("@templateWinCondition", template.winCondition);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            //LinkTemplateToGame(template);
        }

        //public void LinkTemplateToGame(TemplateDTO template)
        //{
        //    using (MySqlConnection con = new MySqlConnection(connString))
        //    {
        //        GameDTO game = new GameDTO();
        //        con.Open();
        //        string query = "INSERT INTO `game-to-template` (game_ID, template_ID) SELECT game.ID, template.ID FROM template JOIN game ON template.game = game.name WHERE template.name = 'Rond de tafel' VALUES (@GameID, @GameName, @TemplateID, @TemplateName)";
        //        using (MySqlCommand cmd = new MySqlCommand(query, con))
        //        {
        //            cmd.Parameters.AddWithValue("@templateGame", template.game);
        //            cmd.Parameters.AddWithValue("@templateName", template.name); // update parameter name to @naam
        //            cmd.Parameters.AddWithValue("@templateMinimumPlayers", template.minimumPlayers);
        //            cmd.Parameters.AddWithValue("@templateRules", template.rules);
        //            cmd.Parameters.AddWithValue("@templateWinCondition", template.winCondition);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        public void DeleteTemplateD(int templateID)
        {
            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                connection.Open();
                string query = "DELETE FROM template WHERE ID = @templateID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@templateID", templateID);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void EditTemplate(TemplateDTO template)
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                string query = "UPDATE template SET game = @game, name = @name, minimum_players = @minimum_players, rules = @rules, win_condition = @win_condition WHERE ID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", template.id);
                    cmd.Parameters.AddWithValue("@game", template.game);
                    cmd.Parameters.AddWithValue("@name", template.name);
                    cmd.Parameters.AddWithValue("@minimum_players", template.minimumPlayers); // update parameter name to @naam
                    cmd.Parameters.AddWithValue("@rules", template.rules);
                    cmd.Parameters.AddWithValue("@win_condition", template.winCondition);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
