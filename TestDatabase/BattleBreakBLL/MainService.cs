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

        public async Task<List<LeaderboardDTO>> GetLeaderboardStats() {
            List<LeaderboardDTO> leaderboardStats = await _mainDAO.GetLeaderboardStats();
           
            return leaderboardStats;


        }
    }
}