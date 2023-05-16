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
        public string Name { get; set; }
        public int Minimum_Players { get; set; }
        public string Rules { get; set; }
        public string Win_Condition { get; set; }
    }
}
