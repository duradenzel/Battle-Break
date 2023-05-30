using BattleBreakBLL.Models;
//using BattleBreakDAL.DTOS;
//using BattleBreakDAL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace BattleBreakBLL
{
    public class AccountService
    {
        public static List<AccountModel> AllAccountsD(int ID, string user_Name, string full_name, string password, string email, string type)
        {
            List<BattleBreakBLL.Models.AccountModel> drinken = new List<BattleBreakBLL.Models.AccountModel>();
            AccountDAL accountDal = new AccountDAL();
            foreach (var item in accountDal.AllAccountsD())
            {
                BattleBreakBLL.Models.AccountModel newItem = new BattleBreakBLL.Models.AccountModel();
                newItem.ID = item.ID;
                newItem.username = item.username;
                newItem.full_name = item.full_name;
                newItem.password = item.password;
                newItem.email = item.email;
                newItem.type = item.type;
                drinken.Add(newItem);
            }

            return (drinken);
        }

        public void MakeAdminL(int ID)
        {
            AccountDAL accountDal = new AccountDAL();
            accountDal.MakeAdminD(ID);
        }

        public void MakeUserL(int ID)
        {
            AccountDAL accountDal = new AccountDAL();
            accountDal.MakeUserD(ID);
        }

    }
}

      