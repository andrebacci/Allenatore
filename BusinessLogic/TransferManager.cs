using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class TransferManager
    {
        private readonly string _connectionString;

        public TransferManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna tutti i trasferimenti
        public async Task<List<Transfers>> GetAll()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Transfers.OrderByDescending(x => x.Date).ToListAsync();
            }
        }

        // Ritorna i trasferimenti di una squadra
        public async Task<List<Transfers>> GetByIdTeam(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Transfers.Where(x => x.IdTeamOld == id || x.IdTeamNew == id).OrderByDescending(x => x.Date).ToListAsync();
            }
        }

        // Ritorna un trasferimento dato il suo id
        public async Task<Transfers> GetById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Transfers.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        // Inserisce un nuovo trasferimento
        public async Task<Transfers> Insert(Transfers transfer)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                await ctx.Transfers.AddAsync(transfer);
                await ctx.SaveChangesAsync();
            }

            return await GetById(transfer.Id);
        }
    }
}
