using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakBLL.Models
{
    public class LeaderboardModel
    {
        public int Account_ID { get; set; }
        public int GespeeldeWedstrijden { get; set; }
        public int GewonnenWedstrijden { get; set; }
        public string AccountEmail { get; set; }
        public string VolledigeNaam { get; set; }


    }
}
