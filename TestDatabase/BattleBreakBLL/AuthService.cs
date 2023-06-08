using BattleBreakDAL;
using BattleBreakDAL.DTOS;

namespace BattleBreakBLL
{

    public class AuthService
    {
        private readonly AuthDAL _authDAL = new AuthDAL();


        public async Task<AccountDTO?> GetAccountByEmailAsync(string email)
        {
            return await _authDAL.GetAccountByEmailAsync(email);
        }

        public async Task<AccountDTO?> GetAccountByUsernameAsync(string username)
        {
            return await _authDAL.GetAccountByEmailAsync(username);
        }

        public bool Authenticate(string email, string password)
        {
            var accountTask = _authDAL.GetAccountByEmailAsync(email);
            accountTask.Wait();
            var account = accountTask.Result;

            if (account == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, account.password);
        }

        public async Task Register(string username, string email, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var account = new AccountDTO
            {
                username = username,
                email = email,
                password = passwordHash
            };

            var existingAccount = await GetAccountByEmailAsync(email);
            if (existingAccount != null)
            {
                throw new ArgumentException("Account with the same email already exists");
            }

            existingAccount = await GetAccountByEmailAsync(username);
            if (existingAccount != null)
            {
                throw new ArgumentException("Account with the same username already exists");
            }

            await _authDAL.AddAccountAsync(account);
        }




























        //public AuthService(){} 

        //public (bool,string) Authenticate(string email, string password) {
        //    var (authResult, userType) = _authDAL.Authenticate(email, password);

        //    return (authResult, userType);
        //}

        //public bool Register(string username, string fullname, string email, string password) {
        //    return _authDAL.Register(username, fullname, email, password);
        //}

        //public string GetUserType(string email) {
        //    return _authDAL.GetUserType(email);
        //}
    }
}
