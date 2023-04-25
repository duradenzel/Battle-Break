using MySql.Data.MySqlClient;
using System.Drawing;

namespace TestDatabase.Models
{
    public class spel
    {
        public string naam;
        public int minimumSpelers;
        public string regels;
        public string winConiditie;


        public spel()
        {

        }

        public spel(string naam, int minimumSpelers, string regels, string winConiditie)
        {
            this.naam = naam;
            this.minimumSpelers = minimumSpelers;
            this.regels = regels;
            this.winConiditie = winConiditie;
        }

    }
}

//test