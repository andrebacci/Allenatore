namespace AllenatoreAPI.Models
{
    public class PlayerStatisticsAPI
    {
        public PlayerStatisticsAPI()
        {
            
        }

        public int Gols { get; set; }

        public int GolsHome { get; set; }

        public int GolsAway { get; set; }

        public int Holders { get; set; }

        public int HoldersHome { get; set; }

        public int HoldersAway { get; set; }

        public int SubstituteIn { get; set; }

        public int SubstituteInHome { get; set; }

        public int SubstituteInAway { get; set; }

        public int SubstituteOut { get; set; }

        public int SubstituteOutHome { get; set; }

        public int SubstituteOutAway { get; set; }

        public int Minutes { get; set; }

        public int MinutesHome { get; set; }

        public int MinutesAway { get; set; }

        // Lista partite giocate con info minuti giocati, gol fatti, entrato/uscito, ammonizioni, espulsioni
    }
}