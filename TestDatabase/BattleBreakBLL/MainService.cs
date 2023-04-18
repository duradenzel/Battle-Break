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

    }
}