using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBreakBLL.Models;
using BattleBreakDAL;
using BattleBreakDAL.DTOS;
using MySql.Data.MySqlClient;


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
                    name = dto.name,
                    minimum_players = dto.minimum_players,
                    rules = dto.rules,
                    win_condition = dto.win_condition,
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

        public GameModel GetGameWithID(int ID) {
            
            GameDTO gameDTO = _gameDAL.GetGameWithID(ID);

            GameModel gameModel = new()
            {
                ID = gameDTO.ID,
                name = gameDTO.name,
                minimum_players = gameDTO.minimum_players,
                rules = gameDTO.rules,
                win_condition = gameDTO.win_condition,
            };

            return gameModel;
        }
        public void GamesAddL(int ID, string name, int minimum_players, string rules, string win_condition)
        {
            GameDAL gamedal = new GameDAL();
            gamedal.GameAddD(ID, name, minimum_players, rules, win_condition);
        }

        public void GameChangeL(int ID, string name, int minimum_players, string rules, string win_condition)
        {
            GameDAL gamedal = new GameDAL();
            gamedal.GameChangeD(ID, name, minimum_players, rules, win_condition);
        }

        public  void DeleteGameL(int ID)
        {
            GameDAL gamedal = new GameDAL();
            gamedal.DeleteGameD(ID);
        }

    }
}
        public async Task SendInvite(string[] accounts) {

            foreach(var account in accounts)
            {      
                await _emailService.SendEmail(account);
            }
        }
    }

    
}
