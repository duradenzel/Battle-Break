using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace TestDatabase.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            ViewData["spelersPlek"] = 12;
            ViewData["spelersOverwinningen"] = 32;

            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            List<int> Account_IDs = new();
            List<int> GewonnenWedstrijden = new();
            List<int> IDs = new();
            List<string> Namen = new();

            using (MySqlConnection con = new(connString))
            {
                con.Open();
                MySqlCommand sqlCom = new("Select `Account_ID`, `GewonnenWedstrijden` From statistieken ORDER BY `GewonnenWedstrijden` DESC LIMIT 3", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();

                while (reader.Read())
                {
                    Account_IDs.Add(reader.GetInt32(0));
                    GewonnenWedstrijden.Add(reader.GetInt32(1));
                }
            }

            using (MySqlConnection con = new(connString))
            {
                con.Open();
                MySqlCommand sqlCom = new("Select `ID`, `Gebruikersnaam` From account", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();

                while (reader.Read())
                {
                    IDs.Add(reader.GetInt32(0));
                    Namen.Add(reader.GetString(1));
                }
            }

            List<string> temp = new();

            for(int i = 0; i < Account_IDs.Count; i++)
            {
                if (Account_IDs[i] == IDs[i])
                {
                    _ = temp.Append(Namen[Account_IDs[i]]);
                }
            }


            ViewData["Namen"] = temp;
            ViewData["Account_IDs"] = Account_IDs;
            ViewData["GewonnenWedstrijden"] = GewonnenWedstrijden;
            return View();
        }
    }
}
