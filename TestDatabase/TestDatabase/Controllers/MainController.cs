using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TestDatabase.Models;
using BattleBreakBLL;
using BattleBreakBLL.Models;
using BCrypt.Net;
using TestDatabase.ViewModels;
using System.Threading.Tasks.Dataflow;

namespace TestDatabase.Controllers
{
    public class MainController : Controller
    {
        private readonly MainService _mainService;
        private readonly MatchService _matchService = new();
        private readonly GameService _gameService = new();
        private readonly AccountService _accountService = new();

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
            List<GameModel> gameModelList = _gameService.GetGames();
            var viewModel = new MainViewModel(leaderboardStats, matchHistory, gameModelList);      

            return View(viewModel);
        }  
           

        public IActionResult Game(string gekozenSpel)
        {
            List<GameModel> gameModels = _gameService.GetGames();
            GameModel ChosenGame = new();


            foreach(var game in gameModels)
            {
                if (gekozenSpel.ToLower() == game.name.ToLower())
                {
                    ChosenGame = game;
                }
            }

            return View(ChosenGame);
        }

        public IActionResult Wedstrijd(int ID)
        {
            List<MatchModel> matchList = _matchService.GetMatchWithID(ID);
            List<AccountModel> accountList = _matchService.GetAccounts(ID);
            GameModel gameModel = _gameService.GetGameWithID(matchList[0].Game_ID);
            MatchViewModel matchViewModel = new(matchList, accountList, gameModel);

            return View(matchViewModel);
        }

        public async Task<IActionResult> Profile(int ID)
        {
            List<LeaderboardModel> leaderboardStats = await _mainService.GetLeaderboardStats();
            List<MatchHistoryModel> individualMatchHistory = await _mainService.GetIndividualMatchHistory(ID);
            List<GameModel> gameModelList = _gameService.GetGames();
            AccountModel account = await _accountService.GetAccountWithIDAsync(ID);

            var viewModel = new ProfileViewModel(leaderboardStats, individualMatchHistory, gameModelList, account);
            return View(viewModel);
        }


        public int sendData(int Game_ID, int User_ID, /*int Won,*/ string User_IDs)
        {
            int Won = 0;
            return _matchService.SendData(Game_ID, User_IDs, Won, 2);
        }

        public void updateMatchData(int Match_ID, string points)
        {
            _matchService.UpdateData(Match_ID, points);
            //return match_ID.ToString() + points;
        }
    }
}

