using BattleBreakBLL;
using BattleBreakBLL.Models;
using BattleBreakDAL.DTOS;
using BattleBreakDAL;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using TestDatabase.Models;

namespace TestDatabase.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Create(int ID, string GameName, int MinimumPlayers, string Regels, string WinCondition)
        {
            // return the create view
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                string query = "INSERT INTO spel (naam, MinimumSpelers, Regels, WinConditie) VALUES (@naam, @minPlayers, @regels, @winCondition)";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@naam", GameName); // update parameter name to @naam
                    cmd.Parameters.AddWithValue("@minPlayers", MinimumPlayers);
                    cmd.Parameters.AddWithValue("@regels", Regels);
                    cmd.Parameters.AddWithValue("@winCondition", WinCondition);
                    cmd.ExecuteNonQuery();
                }
                return View("AddGame");
            }
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
            ListMetGames gamen = new ListMetGames();
            // Your logic here
            // Example: call a method on your model using the passed id
            gamen.AllGames();
            List<Games> Games = gamen.AllGames();
            // ...
            return View("Games", Games);
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

        public IActionResult CreateTemplate()
        {
            return View();
        }
    }

}