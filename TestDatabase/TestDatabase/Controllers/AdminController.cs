using BattleBreakBLL;
using BattleBreakBLL.Models;
using BattleBreakDAL.DTOS;
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
            GameService gamedal = new GameService();
            gamedal.GamesAddL(ID, name, minimum_players, rules, win_condition);

            return RedirectToAction("Games");

        }

        public IActionResult AdminPage() { return View(); }

        public IActionResult Admin(int ID)
        {
            AccountService accountService = new AccountService();
            accountService.MakeAdminL(ID);

            return RedirectToAction("AccountList");
        }



        public IActionResult Gebruiker(int ID)
        {
            AccountService accountService = new AccountService();
            accountService.MakeUserL(ID);

            return RedirectToAction("AccountList");
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
            List<TestDatabase.Models.Account> account = new List<TestDatabase.Models.Account>();
            AccountDAL accountDal = new AccountDAL();
            foreach (var item in accountDal.AllAccountsD())
            {
                TestDatabase.Models.Account newItem = new TestDatabase.Models.Account();
                newItem.ID = item.ID;
                newItem.username = item.username;
                newItem.full_name = item.full_name;
                newItem.password = item.password;
                newItem.email = item.email;
                newItem.type = item.type;
                account.Add(newItem);
            }
            return View("AccountList", account);
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
            gameservice.GameChangeL(ID, name, minimum_players, rules, win_condition);

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
                t.name = tm.name;
                t.minimumPlayers = tm.minimumPlayers;
                t.rules = tm.rules; 
                t.winCondition = tm.winCondition;
                template.Add(t);
            }
            return View(template);
        }

        public IActionResult CreateTemplate(int templateID, string templateName, int templateMinimumPlayers, string templateRules, string templateWinCondition)
        {
            TemplateService templateService = new();
            templateService.TemplateAddL(templateID, templateName, templateMinimumPlayers, templateRules, templateWinCondition);

            return RedirectToAction("Template");
        }

        public IActionResult AddTemplate()
        {
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
    }
}