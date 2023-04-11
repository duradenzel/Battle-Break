using MySql.Data.MySqlClient;
using System.Drawing;

namespace TestDatabase.Models
{
    public class spel
    {
        public string naam;
        public int minimumSpelers;
        public string regels;
        public string winConiditie;

        string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
 

    public spel()
        {

        }

        public spel(string naam, int minimumSpelers, string regels, string winConiditie)
        {
            this.naam = naam;
            this.minimumSpelers = minimumSpelers;
            this.regels = regels;
            this.winConiditie = winConiditie;
        }

        public string sendData()
        {
            string returnstring = "ni";
            using (MySqlConnection con = new(connString))
            {
                con.Open();
                MySqlCommand sqlCom = new("Select `ID`, `Gebruikersnaam` From account", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    returnstring += reader.GetString(1);
                }
            }
            return returnstring;
        }
    }
}
