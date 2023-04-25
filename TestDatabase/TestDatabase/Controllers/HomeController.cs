using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestDatabase.Models;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using BattleBreakBLL;

namespace TestDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthService _authService = new AuthService();

        public HomeController(){}



        public IActionResult Index()
        {
            return View("Login");
        }
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        public IActionResult RegisterPage()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string remember)
        {
           
            var (authResult, userType) = _authService.Authenticate(email, password);

            if (authResult)
            {
                if (userType == "Admin") {
                    return RedirectToAction("Index", "Main");
                }
                return RedirectToAction("Index", "Main");
                
            }
            ViewBag.ErrorMessage = "Incorrect email or password";
            return View();
        }

        

        [HttpPost]
        public IActionResult Register(string username, string fullname, string email, string password)
        {
            if (_authService.Register(username, fullname, email, password))
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }



        public string GetUserType(string email)
        {
            return _authService.GetUserType(email);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}