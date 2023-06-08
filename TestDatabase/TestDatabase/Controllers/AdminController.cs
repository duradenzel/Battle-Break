using BattleBreakBLL;
using BattleBreakBLL.Models;
using BattleBreakDAL.DTOS;
using BattleBreakDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Xml.Linq;
using TestDatabase.Models;
using TestDatabase.ViewModels;

namespace TestDatabase.Controllers
{
    public class AdminController : Controller
    {
        private readonly AccountService _accountService = new();

        public IActionResult GameAdd(int ID, string name, int minimum_players, string rules, string win_condition)
        {

            GameService gamedal = new GameService();
            gamedal.GamesAddL(ID, name, minimum_players, rules, win_condition);
            
            return RedirectToAction("Games");

        }

        public IActionResult AdminPage() 
        { 
            return View(); 
        }

        public IActionResult Admin(int ID)
        {
            AccountService accountService = new AccountService();
            accountService.MakeAdminL(ID);

            return RedirectToAction("Index");
        }



        public IActionResult Gebruiker(int ID)
        {
            AccountService accountService = new AccountService();
            accountService.MakeUserL(ID);

            return RedirectToAction("Index");
        }

        public IActionResult Games()
        {
            List<Games> games = new List<Games>();
            GameService gameservice = new GameService();
            foreach (var item in gameservice.GetGames())
            {
                Games newItem = new Games();
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

        public IActionResult Index()
        {
            List<AccountModel> accountList = _accountService.AllAccountsD();

            return View(accountList);
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

        public IActionResult GameChange(Games games)
        {
            GameService gameservice = new();
            gameservice.GameChangeL(games.ID, games.name, games.minimum_players, games.rules, games.win_condition);

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
            TemplateService templateService = new();
            List<Template> template = new();
            List<TemplateModel> templateModel = templateService.GetTemplates();

            foreach (TemplateModel tm in templateModel)
            {
                Template t = new();
                t.id = tm.id;
                t.game = tm.game;
                t.name = tm.name;
                t.minimumPlayers = tm.minimumPlayers;
                t.rules = tm.rules; 
                t.winCondition = tm.winCondition;
                template.Add(t);
            }

            return View(template);
        }

        public IActionResult CreateTemplate(TemplateModel templateModel)
        {
            GameService gameService = new();
            TemplateService templateService = new TemplateService();
            List<GameModel> gameModelList = gameService.GetGames();

            templateService.TemplateAddL(templateModel);

            return RedirectToAction("Template");
        }

        public IActionResult AddTemplate()
        {
            TemplateService templateService = new TemplateService();
            List<string> gameNames = templateService.GetGames();
            ViewBag.GameNames = new SelectList(gameNames);
            return View();
        }

        public IActionResult DeleteTemplate(int id)
        {
            {
                TemplateService templateService = new();
                templateService.DeleteTemplateL(id);

                return RedirectToAction("Template");
            }
        }

        public IActionResult EditTemplate(int id, string game, string name, int minimumPlayers, string winCondition, string rules)
        {
            Template template = new Template();
            template.id = id;
            template.game= game;
            template.name= name;
            template.minimumPlayers =minimumPlayers;
            template.winCondition = winCondition;
            template.rules = rules;

            return View(template);
        }

    }
}