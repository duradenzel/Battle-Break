using Microsoft.AspNetCore.Mvc;

namespace TestDatabase.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
