using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace TestDatabase.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            List<int> Account_IDs = new();
            List<int> GewonnenWedstrijden = new();
            List<int> IDs = new();
            List<string> NamenTemp = new();

            int spelerID = 0;
            string spelerNaam = "BramvdBallen";
            int spelerGewonnen = 0;
            int spelerPositie = 0;

            using (MySqlConnection con = new(connString))
            {
                con.Open();
                MySqlCommand sqlCom = new("Select `ID`, `Gebruikersnaam` From account", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();

                int i = 0;

                while (reader.Read())
                {
                    IDs.Add(reader.GetInt32(0));
                    NamenTemp.Add(reader.GetString(1));
                    i++;
                    if (reader.GetString(1) == spelerNaam)
                    {
                        spelerID = reader.GetInt32(0);
                        spelerPositie = i;
                    }
                }
            }

            using (MySqlConnection con = new(connString))
            {
                con.Open();
                MySqlCommand sqlCom = new("Select `Account_ID`, `GewonnenWedstrijden` From statistieken ORDER BY `GewonnenWedstrijden` DESC", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();

                while (reader.Read())
                {
                    Account_IDs.Add(reader.GetInt32(0));
                    GewonnenWedstrijden.Add(reader.GetInt32(1));

                    if (reader.GetInt32(0) == spelerID)
                    {
                        spelerGewonnen = reader.GetInt32(1);
                    }
                }
            }

            
            

            List<string> Namen = new();

            for(int i = 0; i < Account_IDs.Count; i++)
            {
                for(int j = 0; j < NamenTemp.Count; j++)
                {
                    if (Account_IDs[i] == IDs[j])
                    {
                        Namen.Add(NamenTemp[j]);
                    }
                }
            }

            ViewData["SpelerNaam"] = spelerNaam;
            ViewData["spelersPlek"] = spelerPositie;
            ViewData["spelersOverwinningen"] = spelerGewonnen;

            ViewData["Namen"] = Namen;
            ViewData["Account_IDs"] = Account_IDs;
            ViewData["GewonnenWedstrijden"] = GewonnenWedstrijden;
            return View();
        }
    }
}
