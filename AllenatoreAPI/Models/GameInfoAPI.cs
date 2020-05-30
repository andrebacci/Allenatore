using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class GameInfoAPI
    {
        public GameInfoAPI()
        {

        }

        public int IdGame { get;set; }

        public int IdTeamHome { get; set; }

        public int IdTeamAway { get; set; }

        public List<PlayerAPI> FormationHome { get; set; }

        public List<PlayerAPI> FormationAway { get; set; }

        public List<PlayerAPI> ScorerHome { get; set; }

        public List<PlayerAPI> ScorerAway { get; set; }
    }
}
