using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL.DTOS
{
    public class TourneyInfoDTO
    {
        public int TourneyId { get; set; }
        public string TourneyName { get; set; }

        public string TourneyGame { get; set; }
        public DateTime TourneyDate { get; set; }

    }
}
