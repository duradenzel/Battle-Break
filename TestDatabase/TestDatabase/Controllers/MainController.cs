using System.Data;
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
