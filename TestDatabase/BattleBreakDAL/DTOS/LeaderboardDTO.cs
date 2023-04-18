using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL.DTOS
{
    public class LeaderboardDTO
    {
        public int GespeeldeWedstrijden { get; set; }
        public int GewonnenWedstrijden { get; set; }
        public string AccountEmail { get; set; }
    }
}
