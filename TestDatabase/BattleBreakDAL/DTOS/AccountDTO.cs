using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL.DTOS
{
    public class AccountDTO
    {
        public int ID { get; set; }
        public string? username { get; set; }
        public string? full_name { get; set;}
        public string? email { get; set;}
        public string? password { get; set;}
        public string? type { get; set;}
    }
}
