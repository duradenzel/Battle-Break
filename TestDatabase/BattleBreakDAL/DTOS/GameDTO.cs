using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL.DTOS
{
    public class GameDTO
    {
        public int ID { get; set; }
        public string? name { get; set; }
        public int minimum_Players { get; set; }
        public string? rules { get; set; }
        public string? win_Condition { get; set; }
    }
}
