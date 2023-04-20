using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestDatabase.Models;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;


namespace TestDatabase.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }


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

        [HttpPost] //Aangezien IActionResult kut is wacht ik met deze verwerken
        public async Task<IActionResult> Login(string email, string password, string remember) //Past Bram niet aan, is voor Denzel
        {
            UserLogic userDao = new UserLogic();

            var (authResult, userType) = userDao.LogicAuthenticate(email, password);

            if (authResult)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, userType)
                };

                var identity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                
                if (userType == "Admin")
                {                   
                    return RedirectToAction("Index", "Main");
                }
                else if (userType == "Gebruiker")
                {
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Incorrect email or password";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Register(string username, string fullname, string email, string password)
        {
            UserLogic userDao = new UserLogic();

            if (userDao.LogicRegister(username, fullname, email, password))
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}