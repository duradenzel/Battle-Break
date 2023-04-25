using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TestDatabase.Models;
using BattleBreakBLL;
using BattleBreakDAL;
using BattleBreakBLL.Models;

using TestDatabase.ViewModels;

namespace TestDatabase.Controllers
{
    public class MainController : Controller
    {
        string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        private readonly MainService _mainService;

        public MainController()
        {
            _mainService = new MainService();
        }

        public async Task<IActionResult> GetLeaderboard()
        {
           
            return View();
        }

        public async Task<IActionResult> Index()
        {

            List<LeaderboardModel> leaderboardStats = await _mainService.GetLeaderboardStats();
            List<MatchHistoryModel> matchHistory = await _mainService.GetMatchHistory();
            var viewModel = new MainViewModel(leaderboardStats, matchHistory);      

            
            return View(viewModel);

        }  
           

        public IActionResult Spel(string gekozenSpel)
        {
            List<spel> spellen = new();
            using (MySqlConnection con = new(connString))
            {
                con.Open();
                MySqlCommand sqlCom = new("Select * From `spel`", con);
                MySqlDataReader reader = sqlCom.ExecuteReader();
                
                //Adds all known spellen to a list of spellen
                while (reader.Read())
                {
                    spel s = new()
                    {
                        naam = reader.GetString(2),
                        minimumSpelers = reader.GetInt32(3),
                        regels = reader.GetString(4),
                        winConiditie = reader.GetString(5)
                    };
                    spellen.Add(s);
                }
            }
            
            foreach(var spel in spellen)
            {
                if (gekozenSpel.ToLower() == spel.naam.ToLower())
                {
                    ViewData["gekozenSpel"] = spel;
                    ViewData["gekozenSpelID"] = 1;
                }
            }

            return View();
        }

        public IActionResult Wedstrijd(int ID)
        {
            ViewData["ID"] = ID;
            List<WedstrijdModel> wedstrijden = new();
            List<string> namen = new();

            using(MySqlConnection con = new(connString))
            {
                MySqlDataReader reader;

                con.Open();
                MySqlCommand wedstrijdCMD = new($"Select * from `wedstrijd` where `ID` = {ID} ", con);
                reader = wedstrijdCMD.ExecuteReader();
                
                while (reader.Read())
                {
                    WedstrijdModel w = new()
                    {
                        ID = reader.GetInt32(0),
                        Spel_ID = reader.GetInt32(1),
                        Account_ID = reader.GetInt32(2),
                        Gewonnen = reader.GetInt32(3),
                        Punten = reader.GetInt32(4)
                    };
                    wedstrijden.Add(w);
                }
                con.Close();

                con.Open();
                MySqlCommand accountCMD = new($"Select `Gebruikersnaam` From `Account` Where `ID` IN (Select `Account_ID` From `Wedstrijd` where `ID` = {ID})", con);
                reader = accountCMD.ExecuteReader();
                while (reader.Read())
                {
                    namen.Add(reader.GetString(0));
                }
            }
            ViewData["namen"] = namen;
            return View(wedstrijden);
        }
         
        public int sendData(int Spel_ID, int User_ID, int Gewonnen, string User_IDs)
        {
            int ID = 0;

            //Convert string of UIDs to String Array
            string[] User_IDList = User_IDs.Split(',');

            using (MySqlConnection con = new(connString))
            {
                MySqlDataReader reader;

                //Get the greatest ID from wedstrijd table
                con.Open();
                    MySqlCommand getIdCom = new("Select `ID` from `wedstrijd` ORDER BY `ID` ASC", con);
                    reader = getIdCom.ExecuteReader();

                    while (reader.Read())
                    {
                        ID = reader.GetInt32(0);
                    }
                con.Close();

                //Increment it so that it's a unique ID
                ID++;

                //Loop through all entries of IDs provided by user on the Spel page
                for (int i = 0; i < User_IDList.Length; i++)
                {
                    con.Open();
                        //Try to add the provided IDs of the users to the Database table
                        try{
                            using (var cmd = new MySqlCommand())
                            {
                                cmd.CommandText = $"INSERT INTO wedstrijd (ID, Spel_ID, Account_ID, Gewonnen, Punten) VALUES ({ID},{Spel_ID},{User_IDList[i]},{Gewonnen}, 2)";
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = con;

                                reader = cmd.ExecuteReader();
                            }
                        } catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    con.Close();
                }
            }
            //Return the wedstrijd ID so that it can be used to fetch all wedstrijd data for the wedstrijd page 
            return ID;
        }
    }
}

