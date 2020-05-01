using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task<List<Feets>> GetByIdRound()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                
            }
        }
    }
}
