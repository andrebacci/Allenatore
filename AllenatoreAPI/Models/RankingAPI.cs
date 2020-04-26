using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class RankingAPI
    {
        public RankingAPI()
        {
            
        }

        public string Team { get; set; }
        
        public int Points { get; set; }

        public int Games { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Loses { get; set; }

        public int GoalMade { get; set; }

        public int GoalConceded { get; set; }

        public int GoalDifference { get; set; }
    }
}
