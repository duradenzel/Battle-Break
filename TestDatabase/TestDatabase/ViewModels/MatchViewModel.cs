using BattleBreakBLL.Models;

namespace TestDatabase.ViewModels
{
    public class MatchViewModel
    {
        public List<MatchModel> MatchList { get; set; }
        public List<AccountModel> AccountList { get; set; }
        public GameModel Game { get; set; }

        public MatchViewModel(List<MatchModel> matchModels, List<AccountModel> accountModels, GameModel gameModel)
        {
            MatchList = matchModels;
            AccountList = accountModels;
            Game = gameModel;
        }
    }
}
