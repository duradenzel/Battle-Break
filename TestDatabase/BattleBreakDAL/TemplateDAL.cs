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

        public List<DTOS.TemplateDTO> GetTemplates()
        {
            using (MySqlConnection con = MakeConnection())
            {
                con.Open();
                MySqlCommand sqlCom = new MySqlCommand("Select * From templates", con);
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

    }
}
