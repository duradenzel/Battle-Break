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
                    t.id = reader.GetInt32(0);
                    t.name = reader.GetString(1);
                    t.minimumPlayers = reader.GetInt32(2);
                    t.rules = reader.GetString(3);
                    t.winCondition = reader.GetString(4);
                    templateList.Add(t);
                }
                return templateList;
            }
        }

        public void TemplateAddD(TemplateDTO template)
        {
            // return the create view
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                string query = "INSERT INTO template (name, minimum_players, rules, win_condition) VALUES (@templateName, @templateMinimumPlayers, @templateRules, @templateWinCondition)";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@templateName", template.name); // update parameter name to @naam
                    cmd.Parameters.AddWithValue("@templateMinimumPlayers", template.minimumPlayers);
                    cmd.Parameters.AddWithValue("@templateRules", template.rules);
                    cmd.Parameters.AddWithValue("@templateWinCondition", template.winCondition);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteTemplateD(int templateID)
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                connection.Open();
                string query = "DELETE FROM template WHERE ID = @templateID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@templateID", templateID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
