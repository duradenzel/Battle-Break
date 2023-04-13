using System.Data.SqlClient;

namespace BattleBreakDAL
{
    public class MainDAO
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";


        public MainDAO() { }

        public async Task<List<PlayerStatsDTO>> GetLeaderboardStats()
        {
            List<PlayerStatsDTO> leaderboardStats = new List<PlayerStatsDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT statistieken.GespeeldeWedstrijden, statistieken.GewonnenWedstrijden, account.Email FROM statistieken INNER JOIN account ON statistieken.Account_ID = account.ID;", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PlayerStatsDTO stats = new PlayerStatsDTO
                                {
                                    GespeeldeWedstrijden = (int)reader["GespeeldeWedstrijden"],
                                    GewonnenWedstrijden = (int)reader["GewonnenWedstrijden"],
                                    AccountEmail = (string)reader["Email"]
                                };
                                leaderboardStats.Add(stats);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            return leaderboardStats;

        }
        public string DALsendData()
        {
            string returnstring = "ni";
            using (SqlConnection con = new(_connString))
            {
                con.Open();
                SqlCommand sqlCom = new("Select `ID`, `Gebruikersnaam` From account", con);
                SqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    returnstring += reader.GetString(1);
                }
            }
            return returnstring;
        }
    }

    
}