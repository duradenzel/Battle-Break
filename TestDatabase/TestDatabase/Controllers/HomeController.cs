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

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string remember)
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            UserLogic userDao = new UserLogic(connString);

            var (authResult, userType) = userDao.LogicAuthenticate(email, password, connString);

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

        //public IActionResult Login(string email, string password, string remember)
        //{
        //    string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
        //    UserDAO userDao = new UserDAO(connString);

        //    if (userDao.Authenticate(email, password))
        //    {
        //        return RedirectToAction("Index", "Main");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Incorrect email or password";
        //        return View();
        //    }
        //}

        [HttpPost]
        public IActionResult Register(string username, string fullname, string email, string password)
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            UserLogic userDao = new UserLogic(connString);

            if (userDao.LogicRegister(username, fullname, email, password))
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }



        public string GetUserType(string email)
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
            UserLogic userDao = new UserLogic(connString);
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    string query = "SELECT Type FROM account WHERE Email = @Email";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        conn.Open();

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error retrieving user type: " + ex.Message);
            }

            return null;
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}