using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestDatabase.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using BattleBreakBLL;
using TestDatabase.ViewModels;

namespace TestDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthService _authService = new AuthService();

        public HomeController(AuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await _authService.GetAccountByEmailAsync(model.email);
                if (account == null)
                {
                    ViewBag.ErrorMessage = "The email you entered is not associated with an account. Please register a new account.";
                    return View();
                }

                if (!_authService.Authenticate(model.email, model.password))
                {
                    ViewBag.ErrorMessage = "Incorrect password.";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, account.ID.ToString()),
                    new Claim(ClaimTypes.Name, account.username),
                    new Claim(ClaimTypes.Email, account.email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                return RedirectToAction("Index", "Main");

            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = await _authService.GetAccountByEmailAsync(model.email);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "A user with this email already exists.";
                return View(model);
            }

            existingUser = await _authService.GetAccountByUsernameAsync(model.username);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "A user with this username already exists.";
                return View(model);
            }


            await _authService.Register(model.username, model.full_name, model.email, model.password);

            var account = await _authService.GetAccountByEmailAsync(model.email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.ID.ToString()),
                new Claim(ClaimTypes.Name, account.username),
                new Claim(ClaimTypes.Email, account.email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            TempData["Message"] = "Account created successfully!";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}