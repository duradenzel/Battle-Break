using BattleBreakBLL.Models;
using BattleBreakDAL;

using BattleBreakDAL.DTOS;

namespace BattleBreakBLL
{
    public class MainService
    {
        private readonly MainDAO _mainDAO;

        public MainService(MainDAO mainDAO)
        {
            _mainDAO = mainDAO;
        }

        public async Task<List<LeaderboardModel>> GetLeaderboardStats()
        {
            List<LeaderboardDTO> leaderboardDTOs = await _mainDAO.GetLeaderboardStats();
            List<LeaderboardModel> leaderboardModels = new List<LeaderboardModel>();

            foreach (var dto in leaderboardDTOs)
            {
                leaderboardModels.Add(new LeaderboardModel
                {
                    GespeeldeWedstrijden = dto.GespeeldeWedstrijden,
                    GewonnenWedstrijden = dto.GewonnenWedstrijden,
                    AccountEmail = dto.AccountEmail
                });
            }

            return leaderboardModels;
        }

        public async Task<List<MatchHistoryModel>> GetMatchHistory()
        {
            List<MatchHistoryDTO> matchHistoryDTOs = await _mainDAO.GetMatchHistory();
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

    }
}