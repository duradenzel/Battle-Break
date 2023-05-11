using System;
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
                    Account_ID = dto.Account_ID,
                    User_Name = dto.User_Name,
                    Full_Name = dto.Full_Name,
                    Email = dto.Email,
                    Password = dto.Password,
                    Type = dto.Type,
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
    }
}
