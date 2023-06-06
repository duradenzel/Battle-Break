using BattleBreakBLL.Models;

namespace TestDatabase.ViewModels
{
    public class TemplateViewModel
    {
        public List<GameModel> GameList { get; set; }
        public TemplateModel id { get; set; }
        public TemplateModel name { get; set; }
        public TemplateModel minimumPlayers { get; set; }
        public TemplateModel rules { get; set; }
        public TemplateModel winCondition { get; set; }

        public TemplateViewModel(List<GameModel> gameList, TemplateModel id, TemplateModel name, TemplateModel minimumPlayers, TemplateModel rules, TemplateModel winCondition)
        {
            GameList = gameList;
            this.id = id;
            this.name = name;
            this.minimumPlayers = minimumPlayers;
            this.rules = rules;
            this.winCondition = winCondition;
        }
    }
}
