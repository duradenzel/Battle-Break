using BattleBreakDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakBLL
{

    public class AuthService
    {
    private readonly AuthDAL _authDAL = new AuthDAL();

        public AuthService(){} 

        public (bool,string) Authenticate(string email, string password) {
            var (authResult, userType) = _authDAL.Authenticate(email, password);

            return (authResult, userType);
        }

        public bool Register(string username, string fullname, string email, string password) {
            return _authDAL.Register(username, fullname, email, password);
        }
    }
}
