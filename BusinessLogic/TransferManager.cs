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
    }
}
