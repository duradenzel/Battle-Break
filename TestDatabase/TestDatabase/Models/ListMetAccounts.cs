//using MySql.Data.MySqlClient;

//namespace TestDatabase.Models
//{
//    public class ListMetAccounts
//    {
//        public List<Account> AllAccounts()
//        {
//            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

//            List<TestDatabase.Models.Account> accounts = new List<TestDatabase.Models.Account>();
//            using (MySqlConnection con = new MySqlConnection(connString))
//            {
//                con.Open();

//                // Query to retrieve data from the database
//                string query = "SELECT * FROM account"; // Replace 'YourTableName' with the actual name of your database table
//                using (MySqlCommand cmd = new MySqlCommand(query, con))
//                {
//                    using (MySqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            // Create a new Account object and populate its properties from the retrieved data
//                            TestDatabase.Models.Account account = new TestDatabase.Models.Account();
//                            account.ID = Convert.ToInt32(reader["ID"]);
//                            account.Email = Convert.ToString(reader["Email"]);
//                            account.VolledigeNaam = Convert.ToString(reader["VolledigeNaam"]);
//                            account.Type = Convert.ToString(reader["Type"]);

//                            // Add the populated Account object to the list
//                            accounts.Add(account);
//                        }
//                    }
//                }
//            }
//            return accounts;
//        }

//    }
//}
