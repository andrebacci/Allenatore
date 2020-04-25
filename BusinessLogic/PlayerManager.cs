using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PlayerManager
    {
        private readonly string _connectionString;

        public PlayerManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritora tutti i giocatori dato l'id di una squadra
        public async Task<List<Players>> GetByTeamId(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Players.Where(x => x.IdTeam == id).OrderBy(x => x.Lastname).ToListAsync();
            }
        }

        // Ritorna il giocatore dato il suo id
        public async Task<Players> GetById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Players.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }
    }
}
