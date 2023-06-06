﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBreakBLL.Models;
using BattleBreakDAL.DTOS;
using BattleBreakDAL;
using MySql.Data.MySqlClient;
using System.Data;

namespace BattleBreakBLL
{
    public class MatchService
    {
        private readonly MatchDAL _matchDAL = new();

        public MatchService() { }

        public List<MatchModel> GetMatchWithID(int ID)
        {
            List<MatchModel> matchModels = new();
            List<MatchDTO> matchDTOs = _matchDAL.GetMatchWithID(ID);

            foreach (var dto in matchDTOs)
            {
                matchModels.Add(new MatchModel
                {
                    Match_ID = dto.Match_ID,
                    Game_ID = dto.Game_ID,
                    Account_ID = dto.Account_ID,
                    Won = dto.Won,
                    Points = dto.Points,
                });
            }
            return matchModels;
        }

        public List<MatchModel> GetMatches()
        {
            List<MatchModel> matchModels = new();
            List<MatchDTO> matchDTOs = _matchDAL.GetMatches();

            foreach (var dto in matchDTOs)
            {
                matchModels.Add(new MatchModel
                {
                    Match_ID = dto.Match_ID,
                    Game_ID = dto.Game_ID,
                    Account_ID = dto.Account_ID,
                    Won = dto.Won,
                    Points = dto.Points,
                });
            }
            return matchModels;
        }

        public List<AccountModel> GetAccounts(int ID) { 
            List<AccountModel> accountModels = new();
            List<AccountDTO> accountDTOs = _matchDAL.GetAccounts(ID);

            foreach (var dto in accountDTOs)
            {
                accountModels.Add(new AccountModel
                {
                    account_ID = dto.ID,
                    username = dto.username,
                    full_name = dto.full_name,
                    email = dto.email,
                    password = dto.password,
                    type = dto.type,
                });
            }
            return accountModels;
        }

        public int SendData(int Game_ID, string User_IDs, int Won, int Score)
        {
            //Convert string of UIDs to String Array
            string[] User_IDList = User_IDs.Split(',');

            int ID = _matchDAL.GetGreatestMatchID();

            //Increment so that it's a unique ID
            ID++;

            //Loop through all entries of IDs provided by user on the Spel page
            for (int i = 0; i < User_IDList.Length; i++)
            {
                int User_ID = int.Parse(User_IDList[i]);
                try
                {
                    _matchDAL.CreateMatch(ID, Game_ID, User_ID, Won, Score);
                }
                catch
                {

                }
            } 

            return ID;
        }

        public void UpdateData(int Match_ID, string points, int winner)
        {
            string[] pointsList = points.Split(',');
            List<MatchModel> matchModels = GetMatchWithID(Match_ID);
            int i = 0;

            foreach (MatchModel match in matchModels)
            {
                if (match.Account_ID == winner)
                {
                    _matchDAL.UpdateMatch(match.Match_ID, match.Account_ID, 1, Int32.Parse(pointsList[i]));
                } else
                {
                    _matchDAL.UpdateMatch(match.Match_ID, match.Account_ID, 0, Int32.Parse(pointsList[i]));

                }
                i++;
            }
        }
    }
}
