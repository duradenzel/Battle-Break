using BattleBreakBLL.Models;

namespace TestDatabase.ViewModels
{
    public class MainViewModel
    {
        public List<LeaderboardModel> LeaderboardModels { get; set; }

        public MainViewModel(List<LeaderboardModel> leaderboardModels) {
            LeaderboardModels = leaderboardModels;
        }
    }
}
