﻿using BattleBreakBLL;
using BattleBreakDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Xml.Linq;
using TestDatabase.Models;

namespace TestDatabase.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult GameAdd(int ID, string name, int minimum_players, string rules, string win_condition)
        {

            GameService gamedal = new GameService();
            gamedal.GamesAddL(ID, name, minimum_players, rules, win_condition);

            return RedirectToAction("Games");
            
        }

        public IActionResult AdminPage() { return View(); }

        public IActionResult Admin(int id)
        {
            Account account = new Account();
            ListMetAccounts l = new ListMetAccounts();
            // Your logic here
            // Example: call a method on your model using the passed id
            account.GetAccountById(id);
            List<Account> accounts = l.AllAccounts();
            // ...

            return View("AccountList", accounts);
        }

        public IActionResult Gebruiker(int id)
        {
            Account account = new Account();
            ListMetAccounts l = new ListMetAccounts();
            // Your logic here
            // Example: call a method on your model using the passed id
            account.MaakGebruiker(id);
            List<Account> accounts = l.AllAccounts();
            // ...
            return View("AccountList", accounts);
        }

        public IActionResult Games()
        {
            List<TestDatabase.Models.Games> games = new List<TestDatabase.Models.Games>();
             GameService gameservice = new GameService();
            foreach (var item in gameservice.GetGames())
            {
                TestDatabase.Models.Games newItem = new TestDatabase.Models.Games();
                newItem.ID = item.ID;
                newItem.name = item.name;
                newItem.win_condition = item.win_condition;
                newItem.rules = item.rules;
                newItem.minimum_players = item.minimum_players;

                games.Add(newItem);
            }
            // ...
            return View("Games", games);
        }

        public IActionResult AccountList()
        {
            ListMetAccounts l = new ListMetAccounts();
            List<Account> accounts =  l.AllAccounts();

            return View("AccountList", accounts);
        }

        public IActionResult AddGame()
        {
            return View();
        }

        public IActionResult GameEdit(int ID, string name, int minimum_players, string rules, string win_condition)
        {
            Games game = new Games();
            game.ID = ID;
            game.name = name;
            game.minimum_players = minimum_players;
            game.rules = rules;
            game.win_condition = win_condition;


            return View(game);
        }

        public IActionResult GameChange(int ID, string name, int minimum_players, string rules, string win_condition)
        {
            GameService gameservice = new GameService();
            gameservice.GameChangeL (ID, name,  minimum_players,  rules,  win_condition);

            return RedirectToAction("Games");
        }

        public IActionResult DeleteGame(int ID)
        {


            GameService gameservice = new GameService();
            gameservice.DeleteGameL(ID);

            return RedirectToAction("Games");
        }
            
        public IActionResult Template()
        {
            return View();
        }
    }
}