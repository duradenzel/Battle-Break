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
        public async Task<AccountModel> GetAccountWithIDAsync(int ID)
        {
            AccountDTO accountDTO = await _accountDAL.GetAccountWithIDAsync(ID);

            AccountModel accountModel = new AccountModel
            {
                account_ID = accountDTO.ID,
                username = accountDTO.username,
                full_name = accountDTO.full_name,
                password = accountDTO.password,
                email = accountDTO.email,
                type = accountDTO.type
            };

            return accountModel;
        }

        public List<AccountModel> AllAccountsD()
        {
            List<AccountModel> accountModels = new();
            foreach (var item in _accountDAL.AllAccountsD())
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
                accountModels.Add(newItem);
            }

            return (accountModels);
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

      