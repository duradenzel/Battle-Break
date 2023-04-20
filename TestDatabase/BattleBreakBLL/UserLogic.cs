using MySql.Data.MySqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using BattleBreakDAL;
using System.Security.Claims;

namespace TestDatabase
{
    public class UserLogic
    {
        public UserLogic()
        {

        }


        public (bool, string) LogicAuthenticate(string email, string password)
        {
            UserDAO UDAO = new UserDAO();
            return UDAO.DALAuthenticate(email, password);
        }

        public bool LogicRegister(string username, string fullname, string email, string password)
        {
            UserDAO UDAO = new UserDAO();
            return UDAO.DALRegister(username, fullname, email, password);
        }
    }
}

