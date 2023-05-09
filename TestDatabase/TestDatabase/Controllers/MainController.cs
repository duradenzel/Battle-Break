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
using System.Threading.Tasks.Dataflow;

namespace TestDatabase.Controllers
{
    public class MainController : Controller
    {
        string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        private readonly MainService _mainService;
        private readonly MatchService _matchService = new();

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
            List<MatchModel> matches = _matchService.GetMatches(ID);
            List<AccountModel> accounts = _matchService.GetAccounts(ID);
            MatchViewModel matchViewModel = new(matches, accounts);

            return View(matchViewModel);
        }

        public int sendData(int Game_ID, int User_ID, /*int Won,*/ string User_IDs)
        {
            int Won = 0;
            return _matchService.SendData(Game_ID, User_IDs, Won, 2);
        }
    }
}

