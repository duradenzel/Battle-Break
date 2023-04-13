using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBreakDAL;

namespace BattleBreakBLL
{
    public class SpelLogic
    {
        public string LogicsendData()
        {
            MainDAO d = new();
            return d.DALsendData();
        }
    }
}
