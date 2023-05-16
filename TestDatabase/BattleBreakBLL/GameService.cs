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
                    minimum_players = dto.minimum_Players,
                    rules = dto.rules,
                    win_condition = dto.win_Condition,
                });
            }
            return gameModels;
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
