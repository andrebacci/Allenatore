using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class PlayerStatistics
    {
        public PlayerStatistics()
        {

        }

        public int GamesAll { get; set; }

        public int Minutes { get; set; }

        public int Gols { get; set; }

        public int GamesHolder { get; set; }

        // Subentrato
        public int GamesIn { get; set; }

        // Sostituito
        public int GamesOut { get; set; }

        public DateTime? LastGame { get; set; }

        public DateTime? LastGameHolder { get; set; }
    }
}
