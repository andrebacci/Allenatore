using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        // Ritorna una presenza dato il suo id
        public async Task<Presences> GetById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Presences.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        // Ritorna i giocatori schierati da una squadra in una partita
        public async Task<List<Presences>> GetByIdGameIdTeam(int idGame, int idTeam)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Presences.Where(x => x.IdGame == idGame && x.IdTeam == idTeam).ToListAsync();
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

        // Inserisce una nuova presenza
        public async Task<Presences> Insert(Presences body)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                await ctx.Presences.AddAsync(body);
                await ctx.SaveChangesAsync();
            }

            return await GetById(body.Id);
        }
    }
}
