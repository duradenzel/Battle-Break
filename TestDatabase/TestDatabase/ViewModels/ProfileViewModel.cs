using BattleBreakBLL.Models;

namespace TestDatabase.ViewModels
{
    public class ProfileViewModel
    {
        public List<LeaderboardModel> Leaderboard { get; set; }
        public List<MatchHistoryModel> MatchHistory { get; set; }
        public List<GameModel> GameList { get; set; }
        public GameModel ChosenGame { get; set; }
        public AccountModel account { get; set; }


        public ProfileViewModel(List<LeaderboardModel> leaderboardModels, List<MatchHistoryModel> matchHistoryModels, List<GameModel> gameList, AccountModel accountModel)
        {
            Leaderboard = leaderboardModels;
            MatchHistory = matchHistoryModels;
            GameList = gameList;
            account = accountModel;
        }

        public ProfileViewModel(List<LeaderboardModel> leaderboardModels, List<MatchHistoryModel> matchHistoryModels, List<GameModel> gameList, AccountModel accountModel, GameModel chosenGame)
        {
            Leaderboard = leaderboardModels;
            MatchHistory = matchHistoryModels;
            GameList = gameList;
            account = accountModel;
            ChosenGame = chosenGame;
        }
    }
}
