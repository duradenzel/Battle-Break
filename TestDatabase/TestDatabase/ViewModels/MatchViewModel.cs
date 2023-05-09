using BattleBreakBLL.Models;

namespace TestDatabase.ViewModels
{
    public class MatchViewModel
    {
        public List<MatchModel> MatchList { get; set; }
        public List<AccountModel> AccountList { get; set; }

        public MatchViewModel(List<MatchModel> matchModels, List<AccountModel> accountModels)
        {
            MatchList = matchModels;
            AccountList = accountModels;
        }
    }
}
