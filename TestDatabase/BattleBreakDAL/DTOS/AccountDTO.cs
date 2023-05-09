using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL.DTOS
{
    public class AccountDTO
    {
        public int Account_ID { get; set; }
        public string? User_Name { get; set; }
        public string? Full_Name { get; set;}
        public string? Email { get; set;}
        public string? Password { get; set;}
        public string? Type { get; set;}
    }
}
