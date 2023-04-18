using BattleBreakDAL.DTOS;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace BattleBreakDAL
{
    public class MainDAO
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";


        public MainDAO() { }

        public async Task<List<LeaderboardDTO>> GetLeaderboardStats()
        {
            List<LeaderboardDTO> leaderboardStats = new List<LeaderboardDTO>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT statistieken.GespeeldeWedstrijden, statistieken.GewonnenWedstrijden, account.Email FROM statistieken INNER JOIN account ON statistieken.Account_ID = account.ID;", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                LeaderboardDTO stats = new LeaderboardDTO();
                                stats.AccountEmail = reader.GetString(reader.GetOrdinal("Email"));
                                stats.GewonnenWedstrijden = reader.GetInt32(reader.GetOrdinal("GewonnenWedstrijden"));
                                stats.GespeeldeWedstrijden = reader.GetInt32(reader.GetOrdinal("GespeeldeWedstrijden"));

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
    }

    
}