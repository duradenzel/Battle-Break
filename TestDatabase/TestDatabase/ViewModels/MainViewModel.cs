using BattleBreakBLL.Models;

namespace TestDatabase.ViewModels
{
    public class MainViewModel
    {
        public List<LeaderboardModel> Leaderboard { get; set; }
        public List<MatchHistoryModel> MatchHistory { get; set; }

        public MainViewModel(List<LeaderboardModel> leaderboardModels, List<MatchHistoryModel> matchHistoryModels)
        {
            Leaderboard = leaderboardModels;
            MatchHistory = matchHistoryModels;
        }
    }
}
