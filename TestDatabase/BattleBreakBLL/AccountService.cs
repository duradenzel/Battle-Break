﻿using BattleBreakBLL.Models;
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

        public AccountModel GetAccountWithID(int ID)
        {
            AccountDTO AccountDTO = _accountDAL.GetAccountWithID(ID);
            AccountModel AccountModel = new()
            {
                account_ID = AccountDTO.ID,
                username = AccountDTO.username,
                full_name = AccountDTO.full_name,
                password = AccountDTO.password,
                email = AccountDTO.email,
                type = AccountDTO.type
            };
            return AccountModel;
        }

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

            return (Account);
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

      