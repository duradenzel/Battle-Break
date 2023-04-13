using BattleBreakDAL;

namespace BattleBreakBLL
{
    public class MainService
    {
        private readonly MainDAO _mainDAO;

        public MainService(MainDAO mainDAO)
        {
            _mainDAO = mainDAO;
        }

        public async Task<List<PlayerStatsDTO>> GetLeaderboardStats() {
        
            List<PlayerStatsDTO> leaderboardStats = await _mainDAO.GetLeaderboardStats();
            return leaderboardStats;
        }
    }
}