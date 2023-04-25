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
        public string Email { get; set; }
        public string VolledigeNaam { get; set; }
        public string Type { get; set; }

        public Account(int iD, string email, string volledigeNaam, string type)
        {
            ID = iD;
            Email = email;
            VolledigeNaam = volledigeNaam;
            Type = type;
        }

        public Account ()
        {

        }
        public void GetAccountById(int id)
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

            List<TestDatabase.Models.Account> accounts = new List<TestDatabase.Models.Account>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                string query = "SELECT * FROM account WHERE ID = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve the current type of the account
                            string currentType = reader.GetString("Type");

                            // Modify the type of the account
                            // You can update the type based on your business logic
                            string newType = "Admin";
                            // You can also retrieve the new type from user input or other sources

                            // Close the data reader before executing another query
                            reader.Close();

                            // Update the type of the account in the database
                            string updateQuery = "UPDATE Account SET Type = @newType WHERE ID = @id";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, con))
                            {
                                updateCmd.Parameters.AddWithValue("@newType", newType);
                                updateCmd.Parameters.AddWithValue("@id", id);
                                updateCmd.ExecuteNonQuery();
                            }

                            // Output a message to indicate the type change
                            Console.WriteLine($"Account with ID {id} has its type changed from {currentType} to {newType}.");
                        }
                        else
                        {
                            // Account with the given ID not found
                            Console.WriteLine($"Account with ID {id} not found.");
                        }
                    }
                }

            }
        }

        public void MaakGebruiker(int id)
        {
            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

            List<TestDatabase.Models.Account> accounts = new List<TestDatabase.Models.Account>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                con.Open();
                string query = "SELECT * FROM account WHERE ID = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve the current type of the account
                            string currentType = reader.GetString("Type");

                            // Modify the type of the account
                            // You can update the type based on your business logic
                            string newType = "Gebruiker";
                            // You can also retrieve the new type from user input or other sources

                            // Close the data reader before executing another query
                            reader.Close();

                            // Update the type of the account in the database
                            string updateQuery = "UPDATE Account SET Type = @newType WHERE ID = @id";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, con))
                            {
                                updateCmd.Parameters.AddWithValue("@newType", newType);
                                updateCmd.Parameters.AddWithValue("@id", id);
                                updateCmd.ExecuteNonQuery();
                            }

                            // Output a message to indicate the type change
                            Console.WriteLine($"Account with ID {id} has its type changed from {currentType} to {newType}.");
                        }
                        else
                        {
                            // Account with the given ID not found
                            Console.WriteLine($"Account with ID {id} not found.");
                        }
                    }
                }

            }
        }

    }

    
}
