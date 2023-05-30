using BattleBreakBLL.Models;

namespace TestDatabase.ViewModels
{
    public class MainViewModel
    {
        public List<LeaderboardModel> Leaderboard { get; set; }
        public List<MatchHistoryModel> MatchHistory { get; set; }
        public List<GameModel> GameList { get; set; }

        public MainViewModel(List<LeaderboardModel> leaderboardModels, List<MatchHistoryModel> matchHistoryModels, List<GameModel> gameList)
        {
            Leaderboard = leaderboardModels;
            MatchHistory = matchHistoryModels;
            GameList = gameList;
        }
    }
}
