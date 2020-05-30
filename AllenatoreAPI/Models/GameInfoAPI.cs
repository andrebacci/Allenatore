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

        public List<PlayerAPI> FormationHome { get; set; }

        public List<PlayerAPI> FormationAway { get; set; }
    }
}
