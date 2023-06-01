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
        private readonly AccountDAL _accountDAL = new();

        public List<AccountModel> AllAccountsD()
        {
            List<AccountModel> drinken = new();
            AccountDAL accountDal = new();
            foreach (var item in accountDal.AllAccountsD())
            {
                AccountModel newItem = new()
                {
                    account_ID = item.ID,
                    username = item.username,
                    full_name = item.full_name,
                    password = item.password,
                    email = item.email,
                    type = item.type
                };
                drinken.Add(newItem);
            }

            return (drinken);
        }

        public void MakeAdminL(int ID)
        {
            _accountDAL.MakeAdminD(ID);
        }

        public void MakeUserL(int ID)
        {
            _accountDAL.MakeUserD(ID);
        }

    }
}

      