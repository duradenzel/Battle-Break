using MySql.Data.MySqlClient;

namespace TestDatabase.Models
{
    public class Account
    {

        //public void ChangeAccountType(int accountId, string newAccountType)
        //{
        //    // Logic to update the account's type in the data store
        //    var account = GetAccountById(accountId);
        //    account.Type = newAccountType;
        //    SaveChangesToDataStore();
        //}

        public int ID { get; set; }
        public string username { get; set; }

        public string full_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string type { get; set; }

        //public Account(int iD, string email, string volledigeNaam, string type)
        //{
        //    ID = iD;
        //    Email = email;
        //    VolledigeNaam = volledigeNaam;
        //    Type = type;
        //}

        public Account ()
        {

        }
        public void GetAccountById(int id)
        {

        }

        public void MaakGebruiker(int id)
        {
           
        }

    }

    
}
