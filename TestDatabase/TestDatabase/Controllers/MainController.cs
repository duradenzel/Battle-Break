using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TestDatabase.Models;
using BattleBreakBLL;
using BattleBreakDAL;
using BattleBreak.Models;

namespace TestDatabase.Controllers
{
    public class MainController : Controller
    {
        string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        private readonly MainService _mainService;

        public MainController()
        {
            _mainService = new MainService(new MainDAO());
        }

        public async Task<IActionResult> GetLeaderboard()
        {
            List<PlayerStats> leaderboardStats = await _mainService.GetLeaderboardStats();
            if (leaderboardStats != null) { 
                return View(leaderboardStats);
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View();
            //string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            //List<int> Account_IDs = new();
            //List<int> GewonnenWedstrijden = new();
            //List<int> IDs = new();
            //List<string> NamenTemp = new();
            
            //int spelerID = -1;
            //string spelerNaam = "BramvdBallen"; //ingelogde speler is nuu nog hardcoded, moet dynamisch
            //int spelerOverwinningen = -1;
            //int spelerPositie = -1;

            //using (MySqlConnection con = new(connString))
            //{
            //    con.Open();
            //    MySqlCommand sqlCom = new("Select `ID`, `Gebruikersnaam` From account", con);
            //    MySqlDataReader reader = sqlCom.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        IDs.Add(reader.GetInt32(0));
            //        NamenTemp.Add(reader.GetString(1));
                    
            //        if (reader.GetString(1) == spelerNaam)
            //        {
            //            spelerID = reader.GetInt32(0);
            //        }
            //    }
            //}

            //using (MySqlConnection con = new(connString))
            //{
            //    con.Open();
            //    MySqlCommand sqlCom = new("Select `Account_ID`, `GewonnenWedstrijden` From statistieken ORDER BY `GewonnenWedstrijden` DESC", con);
            //    MySqlDataReader reader = sqlCom.ExecuteReader();

            //    int i = 0;

            //    while (reader.Read())
            //    {

            //        Account_IDs.Add(reader.GetInt32(0));
            //        GewonnenWedstrijden.Add(reader.GetInt32(1));

            //        i++;

            //        if (reader.GetInt32(0) == spelerID)
            //        {
            //            spelerOverwinningen = reader.GetInt32(1);
            //            spelerPositie = i;
            //        }
            //    }
            //}

            //List<string> Namen = new();

            //for(int i = 0; i < Account_IDs.Count; i++)
            //{
            //    for(int j = 0; j < NamenTemp.Count; j++)
            //    {
            //        if (Account_IDs[i] == IDs[j])
            //        {
            //            Namen.Add(NamenTemp[j]);
            //        }
            //    }
            //}

            //ViewData["SpelerNaam"] = spelerNaam;
            //ViewData["SpelerPositie"] = spelerPositie;
            //ViewData["SpelerOverwinningen"] = spelerOverwinningen;

            //ViewData["Namen"] = Namen;
            //ViewData["Account_IDs"] = Account_IDs;
            //ViewData["GewonnenWedstrijden"] = GewonnenWedstrijden;
            //return View();
        }

        public IActionResult Spel(string gekozenSpel)
        {
            List<spel> spellen = new();
            using (MySqlConnection con = new(connString))
            {
                con.Open();
                MySqlCommand sqlCom = new("Select * From `spel`", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();
                

                while (reader.Read())
                {
                    spel s = new()
                    {
                        naam = reader.GetString(1),
                        minimumSpelers = reader.GetInt32(2),
                        regels = reader.GetString(3),
                        winConiditie = reader.GetString(4)
                    };
                    spellen.Add(s);
                }
            }

            foreach (var spel in spellen)
            {
                if (gekozenSpel.ToLower() == spel.naam.ToLower())
                {
                    ViewData["gekozenSpel"] = spel;
                }
            }

            return View();
        }
    }
}
