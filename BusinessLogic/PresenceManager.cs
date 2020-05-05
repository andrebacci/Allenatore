using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PresenceManager
    {
        private readonly string _connectionString;

        public PresenceManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna i giocatori schierati da una squadra in una partita
        public async Task<List<Presences>> GetByIdRound(int idRound, int idTeam)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Presences.Where(x => x.Id == idRound && x.IdTeam == idTeam).ToListAsync();
            }
        }

        // Ritorna tutte le presenze di un giocatore
        public async Task<List<Presences>> GetPlayedByIdPlayer(int idPlayer)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Presences.Where(x => x.IdPlayer == idPlayer && x.TimeIn != null).ToListAsync();
            }
        }
    }
}
