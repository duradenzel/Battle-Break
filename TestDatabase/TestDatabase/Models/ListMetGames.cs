﻿//using MySql.Data.MySqlClient;

//namespace TestDatabase.Models
//{
//    public class ListMetGames
//    {
//        public List<Games> AllGames()
//        {
//            string connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";

//            List<TestDatabase.Models.Games> Games = new List<TestDatabase.Models.Games>();
//            using (MySqlConnection con = new MySqlConnection(connString))
//            {
//                con.Open();

//                // Query to retrieve data from the database
//                string query = "SELECT * FROM game"; // Replace 'YourTableName' with the actual name of your database table
//                using (MySqlCommand cmd = new MySqlCommand(query, con))
//                {
//                    using (MySqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            // Create a new Account object and populate its properties from the retrieved data
//                            TestDatabase.Models.Games Game = new TestDatabase.Models.Games();
//                            Game.ID = Convert.ToInt32(reader["ID"]);
//                            Game.name = Convert.ToString(reader["name"]);
//                            Game.minimum_players = Convert.ToInt32(reader["minimum_players"]);
//                            Game.Regels = Convert.ToString(reader["rules"]);
//                            Game.WinCondition = Convert.ToString(reader["win_condition"]);

//                            // Add the populated Account object to the list
//                            Games.Add(Game);
//                        }
//                    }
//                }
//            }
//            return Games;
//        }
//    }
//}
