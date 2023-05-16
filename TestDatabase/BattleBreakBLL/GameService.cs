﻿using System;
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

        public GameModel GetGameWithID(int ID) {
            
            GameDTO gameDTO = _gameDAL.GetGameWithID(ID);

            GameModel gameModel = new()
            {
                ID = gameDTO.ID,
                Name = gameDTO.Name,
                Minimum_Players = gameDTO.Minimum_Players,
                Rules = gameDTO.Rules,
                Win_Condition = gameDTO.Win_Condition,
            };

            return gameModel;
        }
    }
}
