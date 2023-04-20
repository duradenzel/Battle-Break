using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakBLL.Models
{
    public class MatchHistoryModel
    {
        public int Game_ID { get; set; }
        public int Match_ID { get; set; }
        public string Player1 { get; set; }
        public int Player1_Points { get; set; }
        public string Player2 { get; set; }
        public int Player2_Points { get; set; }
    }
}
