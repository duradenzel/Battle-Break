using BattleBreakDAL;
using BattleBreak.Models;

namespace BattleBreakBLL
{
    public class MainService
    {
        private readonly MainDAO _mainDAO;

        public MainService(MainDAO mainDAO)
        {
            _mainDAO = mainDAO;
        }

        public async Task<List<PlayerStats>> GetLeaderboardStats() {
        
            List<PlayerStats> leaderboardStats = await _mainDAO.GetLeaderboardStats();
            return leaderboardStats;
        }
    }
}