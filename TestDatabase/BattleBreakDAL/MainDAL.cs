using BattleBreakDAL.DTOS;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace BattleBreakDAL
{
    public class MainDAL
    {
        private readonly string _connString = "Server=studmysql01.fhict.local;Database=dbi515074;Uid=dbi515074;Pwd=AmineGPT;";


        public MainDAL() { }

        public async Task<List<LeaderboardDTO>> GetLeaderboardStats()
        {
            List<LeaderboardDTO> leaderboardStats = new List<LeaderboardDTO>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT stats.matches_played, stats.matches_won, stats.account_ID, account.email, account.full_name FROM stats INNER JOIN account ON stats.account_ID = account.ID ORDER BY stats.matches_won DESC;", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                LeaderboardDTO stats = new LeaderboardDTO();
                                stats.Account_ID = reader.GetInt32(reader.GetOrdinal("account_ID"));
                                stats.AccountEmail = reader.GetString(reader.GetOrdinal("email"));
                                stats.GewonnenWedstrijden = reader.GetInt32(reader.GetOrdinal("matches_won"));
                                stats.GespeeldeWedstrijden = reader.GetInt32(reader.GetOrdinal("matches_played"));
                                stats.VolledigeNaam = reader.GetString(reader.GetOrdinal("full_name"));


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

        public async Task<List<MatchHistoryDTO>> GetMatchHistory()
        {
            List<MatchHistoryDTO> matchHistory = new List<MatchHistoryDTO>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT w1.ID AS match_ID, w1.game_ID AS game_ID, a1.full_name AS Player1, w1.points AS Player1_Points,\r\na2.full_name AS Player2, w2.points AS Player2_Points\r\nFROM `match` w1\r\nJOIN  `match` w2 ON w1.ID = w2.ID AND w1.account_ID < w2.account_ID\r\nJOIN account a1 ON w1.account_ID = a1.ID\r\nJOIN account a2 ON w2.account_ID = a2.ID;", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                MatchHistoryDTO match = new MatchHistoryDTO();
                                match.Game_ID = reader.GetInt32(reader.GetOrdinal("Game_ID"));
                                match.Match_ID = reader.GetInt32(reader.GetOrdinal("Match_ID"));
                                match.Player1 = reader.GetString(reader.GetOrdinal("Player1"));
                                match.Player1_Points = reader.GetInt32(reader.GetOrdinal("Player1_Points"));
                                match.Player2 = reader.GetString(reader.GetOrdinal("Player2"));
                                match.Player2_Points = reader.GetInt32(reader.GetOrdinal("Player2_Points"));


                                matchHistory.Add(match);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            return matchHistory;

        }

        public async Task<List<TourneyInfoDTO>> GetTourneyInfo()
        {
            List<TourneyInfoDTO> tourneyList = new List<TourneyInfoDTO>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM `tournament`", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                TourneyInfoDTO tourney = new TourneyInfoDTO();
                                tourney.TourneyId = reader.GetInt32(reader.GetOrdinal("ID"));
                                tourney.TourneyName = reader.GetString(reader.GetOrdinal("TourneyName"));
                                tourney.TourneyDate = reader.GetDateTime(reader.GetOrdinal("Datum"));



                                tourneyList.Add(tourney);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            return tourneyList;

        }
    }
}