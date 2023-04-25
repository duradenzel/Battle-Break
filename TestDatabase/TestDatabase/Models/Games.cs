using MySql.Data.MySqlClient;

namespace TestDatabase.Models
{
    public class Games
    {
        public int ID   { get; set; }
        public string GameName { get; set; }
        public int MinimumPlayers { get; set; }
        public string Regels   { get; set; }
        public string WinCondition { get; set; }


       
    }
}
