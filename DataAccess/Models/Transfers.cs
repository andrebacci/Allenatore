using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Transfers
    {
        public int Id { get; set; }
        public int IdPlayer { get; set; }
        public int? IdTeamOld { get; set; }
        public int? IdTeamNew { get; set; }
        public DateTime? Date { get; set; }

        public virtual Players IdPlayerNavigation { get; set; }
        public virtual Teams IdTeamNewNavigation { get; set; }
        public virtual Teams IdTeamOldNavigation { get; set; }
    }
}
