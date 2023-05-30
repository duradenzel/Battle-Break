using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBreakBLL.Models;
using BattleBreakDAL;
using BattleBreakDAL.DTOS;


namespace BattleBreakBLL
{
    public class GameService
    {
        private readonly GameDAL _gameDAL = new();
        public readonly MatchDAL _matchDAL = new();
        private readonly EmailService _emailService = new();

        public List<GameModel> GetGames()
        {
            List<GameModel> gameModels = new();
            List<GameDTO> gameDTOs = _gameDAL.GetGames();

            foreach (var dto in gameDTOs)
            {
                gameModels.Add(new GameModel
                {
                    ID = dto.ID,
                    Name = dto.Name,
                    Minimum_Players = dto.Minimum_Players,
                    Rules = dto.Rules,
                    Win_Condition = dto.Win_Condition,
                });
            }
          
            return gameModels;
        }

        public async Task<List<AccountModel>> GetAccounts() {
            List<AccountDTO> accountDTO = await _gameDAL.GetAccounts();
            List<AccountModel> accountModels = new();

            foreach (var DTO in accountDTO)
            {
                accountModels.Add(new AccountModel
                {
                    Account_ID = DTO.Account_ID,
                    User_Name = DTO.User_Name,
                    Full_Name = DTO.Full_Name,
                    Email = DTO.Email
                });
            }

            return accountModels;
        
        }

        public async Task SendInvite(string[] accounts) {

            foreach(var account in accounts)
            {      
                await _emailService.SendEmail(account);
            }
        }
    }

    
}
