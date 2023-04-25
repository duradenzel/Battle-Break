using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL.DTOS
{
    public class MatchDTO
    {
        public int Match_ID { get; set; }
        public int Game_ID { get; set; }
        public int Account_ID { get; set; }
        public int Won { get; set; }
        public int Points { get; set; }
    }
}
