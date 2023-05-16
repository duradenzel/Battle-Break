﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakDAL.DTOS
{
    public class TemplateDTO
    {
        //properties
        public int id { get; set; }
        public string name { get; set; }
        public int minimumPlayers { get; set; }
        public string rules { get; set; }
        public string winCondition { get; set; }

        //constructors
        public TemplateDTO()
        {

        }
    }
}