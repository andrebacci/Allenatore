using System.Collections.Generic;

namespace AllenatoreAPI.Models
{
    public class TeamStatisticsAPI
    {
        public TeamStatisticsAPI()
        {
            HistoryRanking = new List<int>();
        }

        public int Points { get; set; } 

        public int PointsHome { get; set; }

        public int PointsAway { get; set; }

        public int Games { get; set; }

        public int GamesHome { get; set; }

        public int GamesAway { get; set; }

        public int Wins { get; set; }

        public int WinsHome { get; set; }

        public int WinsAway { get; set; }

        public int Draws { get; set; }

        public int DrawsHome { get; set; } 

        public int DrwasAway { get; set; }

        public int Losts { get; set; }

        public int LostsHome { get; set; }

        public int LostsAway { get; set; }

        public int ScoredGols { get;set; }

        public int ScoredGolsHome { get; set; }

        public int ScoredGolsAway { get; set; }

        public int ConcededGols { get; set; }

        public int ConcededGolsHome { get; set; }

        public int ConcededGolsAway { get; set; }

        public PlayerStatisticsAPI BestScorer { get; set; }

        public int ActualPosition { get; set; }

        public List<int> HistoryRanking { get; set; }
    }
}