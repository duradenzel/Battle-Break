﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakBLL.Models
{
    public class AccountModel
    {
        public int account_ID { get; set; }
        public string? user_Name { get; set; }
        public string? full_Name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? type { get; set; }
    }
}
