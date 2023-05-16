using MySql.Data.MySqlClient;

namespace TestDatabase.Models
{
    public class Games
    {
        public int ID   { get; set; }
        public string name { get; set; }
        public int minimum_players { get; set; }
        public string rules   { get; set; }
        public string win_condition { get; set; }


       
    }
}
