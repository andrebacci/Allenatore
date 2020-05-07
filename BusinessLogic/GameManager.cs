using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class GameManager
    {
        private readonly string _connectionString;

        public GameManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna tutte le partite
        public async Task<List<Games>> GetAll()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Games.ToListAsync();
            }
        }

        // Ritorna le partite in un range di giornate
        public async Task<List<Games>> GetRange(int? start, int? end)
        {
            using (POContext ctx = new POContext(_connectionString))
            {
                if (start == end && start > 0 && end > 0)
                    return await ctx.Games.Where(x => x.Round == start).ToListAsync();
                      
                if (start == 0)
                {
                    if (end == 0)
                        return await ctx.Games.ToListAsync();
                    
                    return await ctx.Games.Where(x => x.Round <= end).ToListAsync();
                }

                if (end == 0)
                {
                    if (start == 0)
                        return await ctx.Games.ToListAsync();
                    
                    return await ctx.Games.Where(x => x.Round >= start && x.Round <= end).ToListAsync();
                }
            }

            return null;
        }

        // Ritorna tutte le partite per una determinata giornata
        public async Task<List<Games>> GetByRound(int round)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Games.Where(x => x.Round.GetValueOrDefault() == round).ToListAsync();
            }
        }

        // Ritorna una partita dato il suo id
        public async Task<Games> GetById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Games.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        // Ritorna tutte le partite dato l'id di una squadra
        public async Task<List<Games>> GetByIdTeam(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Games.Where(x => x.IdTeamHome == id || x.IdTeamAway == id).OrderBy(x => x.Round).ToListAsync();
            }
        }

        // Ritorna la partita dato l'id della squadra in casa e della squadra in trasferta
        public async Task<Games> GetByIdTeams(int idTeamHome, int idTeamAway)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Games.Where(x => x.IdTeamHome == idTeamHome && x.IdTeamAway == idTeamAway).FirstOrDefaultAsync();
            }
        }

        // Inserisce uan nuova partita
        public async Task<Games> Insert(Games game)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                await ctx.Games.AddAsync(game);
                await ctx.SaveChangesAsync();
            }

            return await GetById(game.Id);
        }

        // Aggiorna una partita
        public async Task<Games> Update(Games game)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                ctx.Games.Update(game);
                await ctx.SaveChangesAsync();
            }

            return await GetById(game.Id);
        }
    }
}
