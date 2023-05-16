﻿using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TestDatabase.Models;
using BattleBreakBLL;
using BattleBreakDAL;
using BattleBreakBLL.Models;
using BCrypt.Net;
using TestDatabase.ViewModels;
using System.Threading.Tasks.Dataflow;

namespace TestDatabase.Controllers
{
    public class MainController : Controller
    {
        string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

        private readonly MainService _mainService;
        private readonly MatchService _matchService = new();
        private readonly GameService _gameService = new();

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
            List<MatchModel> matches = _matchService.GetMatchWithID(ID);
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

