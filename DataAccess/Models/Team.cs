using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Mister { get; set; }

        public string Category { get; set; }

        public string Logo { get; set; }
    }
}
