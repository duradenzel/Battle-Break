using BattleBreakBLL;
using BattleBreakBLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace TestDatabase.Controllers
{
    public class GameController : Controller
    {
        public readonly GameService _gameService = new GameService();

        [Authorize]

        public IActionResult Index()
        {
            return View();
        }


        public async Task<List<AccountModel>> GetAccounts(){
            List<AccountModel> accounts = await _gameService.GetAccounts();
            return accounts;
        }

       
        public void SendInvite([FromBody] string[] accounts) {

             _gameService.SendInvite(accounts);
                       
        }
    }
}
