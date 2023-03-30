using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestDatabase.Models;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }


        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult RegisterPage()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string remember)
        {

            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            UserDAO userDao = new UserDAO(connString);


            if (userDao.Authenticate(email, password))
            {

                return RedirectToAction("Index", "Main");
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
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            UserDAO userDao = new UserDAO(connString);

            if (userDao.Register(username, fullname, email, password))
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }



        private bool ValidateRememberMeToken()
        {
            if (HttpContext.Request.Cookies.TryGetValue("RememberMeToken", out string token) &&
                HttpContext.Request.Cookies.TryGetValue("RememberMeEmail", out string email))
            {
                string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    string query = "SELECT COUNT(*) FROM AuthTokens WHERE Token = @Token AND Email = @Email";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Token", token);
                        command.Parameters.AddWithValue("@Email", email);
                        conn.Open();

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count > 0)
                        {
                            var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var authProperties = new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddDays(30) };
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties).Wait();

                            return true;
                        }
                    }
                }
            }

            return false;
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}