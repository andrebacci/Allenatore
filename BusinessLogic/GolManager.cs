﻿using DataAccess.Models;
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
        public async Task<List<Gols>> GetByIdPlayer(int idPlayer)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Gols.Where(x => x.IdPlayer == idPlayer).ToListAsync();
            }
        }       
        
        // Ritorna i gol di un giocatore in una partita
        public async Task<List<Gols>> GetGolByIdPlayerIdGame(int idPlayer, int idGame)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Gols.Where(x => x.IdPlayer == idPlayer && x.IdGame == idGame).ToListAsync();
            }
        }

        // Ritorna i gol di una squadra in una partita
        public async Task<List<Gols>> GetGolByIdTeamIdGame(int idTeam, int idGame)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Gols.Where(x => x.IdTeam == idTeam && x.IdGame == idGame).ToListAsync();
            }
        }

        // Ritorna tutti i gol
        public async Task<List<Gols>> GetAll()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Gols.ToListAsync();
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
