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
    public class GolManager
    {
        private readonly string _connectionString;

        public GolManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna un gol dato il suo id
        public async Task<Gols> GetById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Gols.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        // Ritorna tutti i gol di un giocatore
        public async Task<Gols> GetByIdPlayer(int idPlayer)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Gols.Where(x => x.IdPlayer == idPlayer).FirstOrDefaultAsync();
            }
        }        

        // Inserisce una nuova presenza
        public async Task<Gols> Insert(Gols body)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                await ctx.Gols.AddAsync(body);
                await ctx.SaveChangesAsync();
            }

            return await GetById(body.Id);
        }
    }
}
