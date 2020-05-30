namespace AllenatoreAPI.Models
{
    public class ScorerAPI
    {
        public ScorerAPI()
        {

        }

        public int IdPlayer { get; set; }        

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Fullname { get; set; }

        public string Teamname { get; set; }

        public int Gols { get; set; }

        public int? Minute { get; set; }

        public string Description { get; set; }
    }
}
