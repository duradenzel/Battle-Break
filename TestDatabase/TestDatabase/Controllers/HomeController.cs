using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestDatabase.Models;
using MySql.Data.MySqlClient;
using BCrypt.Net;
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
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.User.Identity.IsAuthenticated && ValidateRememberMeToken())
            {
                // User is not authenticated and no valid "RememberMeToken" cookie was found
                // Redirect the user to the login page
                context.Result = RedirectToAction("Index", "Main");
            }

            base.OnActionExecuting(context);
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
        [HttpPost]
        public IActionResult Login(string email, string password, string remember)
        {
            bool rememberMe = false;
            if (!string.IsNullOrEmpty(remember))
            {
                rememberMe = bool.Parse(remember);
            }
            try
            {
                string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    string query = "SELECT Wachtwoord FROM account WHERE Email = @Email";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        conn.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string passwordHash = reader.GetString("Wachtwoord");

                                if (BCrypt.Net.BCrypt.Verify(password, passwordHash))
                                {

                                    if (rememberMe)
                                    {
                                        string token = Guid.NewGuid().ToString();
                                        Response.Cookies.Append("RememberMeToken", token, new CookieOptions { Expires = DateTime.Now.AddDays(30) });
                                        Response.Cookies.Append("RememberMeEmail", email, new CookieOptions { Expires = DateTime.Now.AddDays(30) });

                                        reader.Close(); // Close the data reader before executing the insert query

                                        string tokenQuery = "INSERT INTO authtokens (Token, Email) VALUES (@Token, @Email)";
                                        using (MySqlCommand insertCommand = new MySqlCommand(tokenQuery, conn))
                                        {
                                            insertCommand.Parameters.AddWithValue("@Token", token);
                                            insertCommand.Parameters.AddWithValue("@Email", email);
                                            insertCommand.ExecuteNonQuery();
                                        }
                                    }


                                    return RedirectToAction("Index", "Main");
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = "Incorrect password";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "No account matches the email: " + email;
                                return View();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error during login: " + ex.Message);
            }
            return View();
        }


        [HttpPost]
        public IActionResult Register(string username, string fullname, string email, string password)
        {
            try
            {
                string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

                    string query = "INSERT INTO account (Gebruikersnaam, VolledigeNaam, Email, Wachtwoord) VALUES (@Gebruikersnaam, @VolledigeNaam, @Email, @Wachtwoord)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Gebruikersnaam", username);
                        command.Parameters.AddWithValue("@VolledigeNaam", fullname);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Wachtwoord", passwordHash);
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Debug.Write("Record inserted successfully");
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Debug.Write("Error inserting record");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error during registration: " + ex.Message);
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