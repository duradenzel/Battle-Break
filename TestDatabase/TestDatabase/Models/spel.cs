using MySql.Data.MySqlClient;
using System.Drawing;
using BattleBreakBLL;

namespace TestDatabase.Models
{
    public class Spel
    {
        public string naam;
        public int minimumSpelers;
        public string regels;
        public string winConiditie;

        string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";
 

    public Spel()
        {

        }

        public Spel(string naam, int minimumSpelers, string regels, string winConiditie)
        {
            this.naam = naam;
            this.minimumSpelers = minimumSpelers;
            this.regels = regels;
            this.winConiditie = winConiditie;
        }

        public string sendData()
        {
            SpelLogic spelLogic = new();
            return spelLogic.LogicsendData();
        }
    }
}
