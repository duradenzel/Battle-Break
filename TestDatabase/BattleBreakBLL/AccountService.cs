using BattleBreakBLL.Models;
using BattleBreakDAL.DTOS;
using BattleBreakDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakBLL
{
    public class AccountService
    {
        public static List<AccountModel> AllAccountsD()
        {
            List<BattleBreakBLL.Models.AccountModel> drinken = new List<BattleBreakBLL.Models.AccountModel>();
            AccountDAL accountDal = new AccountDAL();
            foreach (var item in accountDal.AllAccounts)
            {
                MuscleOnlineLogic.DrinkenModel newItem = new MuscleOnlineLogic.DrinkenModel();
                newItem.id = item.id;
                newItem.eiwitten = item.eiwitten;
                newItem.drinken_naam = item.drinken_naam;
                newItem.vetten = item.vetten;
                newItem.koolhydraten = item.koolhydraten;
                newItem.calorieën = item.calorieën;
                Console.WriteLine(item.id);
                drinken.Add(newItem);
            }

            return (drinken);
        }

    }
}
