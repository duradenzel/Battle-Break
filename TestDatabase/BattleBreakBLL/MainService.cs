using BattleBreakBLL.Models;
using BattleBreakDAL;

using BattleBreakDAL.DTOS;

namespace BattleBreakBLL
{
    public class MainService
    {
        private readonly MainDAL _mainDAL = new MainDAL();

        public MainService(){}

        public async Task<List<LeaderboardModel>> GetLeaderboardStats()
        {
            List<LeaderboardDTO> leaderboardDTOs = await _mainDAL.GetLeaderboardStats();
            List<LeaderboardModel> leaderboardModels = new List<LeaderboardModel>();

            foreach (var dto in leaderboardDTOs)
            {
                leaderboardModels.Add(new LeaderboardModel
                {
                    Account_ID = dto.Account_ID,
                    GespeeldeWedstrijden = dto.GespeeldeWedstrijden,
                    GewonnenWedstrijden = dto.GewonnenWedstrijden,
                    AccountEmail = dto.AccountEmail,
                    VolledigeNaam = dto.VolledigeNaam
                });
            }

            return leaderboardModels;
        }

        public async Task<List<MatchHistoryModel>> GetMatchHistory()
        {
            List<MatchHistoryDTO> matchHistoryDTOs = await _mainDAL.GetMatchHistory();
            List<MatchHistoryModel> matchHistoryModels = new List<MatchHistoryModel>();

            foreach (var dto in matchHistoryDTOs)
            {
                matchHistoryModels.Add(new MatchHistoryModel
                {
                    Game_ID = dto.Game_ID,
                    Match_ID = dto.Match_ID,
                    Player1 = dto.Player1,
                    Player1_Points = dto.Player1_Points,
                    Player2 = dto.Player2,
                    Player2_Points = dto.Player2_Points
                });
            }
            return matchHistoryModels;
        }
        public async Task<List<MatchHistoryModel>> GetIndividualMatchHistory(int currentUserID)
        {
            List<MatchHistoryDTO> matchHistoryDTOs = await _mainDAL.GetIndividualMatchHistory(currentUserID);
            List<MatchHistoryModel> individualMatchHistory = new List<MatchHistoryModel>();

            foreach (var dto in matchHistoryDTOs)
            {
                individualMatchHistory.Add(new MatchHistoryModel
                {
                    Game_ID = dto.Game_ID,
                    Match_ID = dto.Match_ID,
                    Player1 = dto.Player1,
                    Player1_Points = dto.Player1_Points,
                    Player2 = dto.Player2,
                    Player2_Points = dto.Player2_Points
                });
            }
            return individualMatchHistory;
        }

        public async Task<List<MatchHistoryModel>> GetIndividualMatchHistoryPerGame(int currentUserID, int selectedGameID)
        {
            List<MatchHistoryDTO> matchHistoryDTOs = await _mainDAL.GetIndividualMatchHistoryPerGame(currentUserID, selectedGameID);
            List<MatchHistoryModel> individualMatchHistory = new List<MatchHistoryModel>();

            foreach (var dto in matchHistoryDTOs)
            {
                individualMatchHistory.Add(new MatchHistoryModel
                {
                    Game_ID = dto.Game_ID,
                    Match_ID = dto.Match_ID,
                    Player1 = dto.Player1,
                    Player1_Points = dto.Player1_Points,
                    Player2 = dto.Player2,
                    Player2_Points = dto.Player2_Points
                });
            }
            return individualMatchHistory;
        }
    }
}