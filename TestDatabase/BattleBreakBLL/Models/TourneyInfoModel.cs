using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakBLL.Models
{
    public class TourneyInfoModel
    {
        public int TourneyId { get; set; }
        public string TourneyName { get; set; }

        public string TourneyGame { get; set; }
        public DateTime TourneyDate { get; set; }
    }
}
