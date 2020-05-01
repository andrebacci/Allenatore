using AllenatoreAPI.Models;
using AllenatoreAPI.Result;
using AllenatoreAPI.Utils;
using BusinessLogic;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AllenatoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public GameController()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }        

        /// <summary>
        /// Ritorna tutte le partite per una data giornata
        /// </summary>
        /// <returns></returns>
        [Route("GetByRound")]
        [HttpGet]
        public async Task<IActionResult> GetByRound([FromQuery] int round)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GameManager manager = new GameManager(_connectionString);
                List<Games> games = await manager.GetByRound(round);

                List<GameAPI> gamesApi = new List<GameAPI>();

                foreach (Games g in games)
                {
                    GameAPI ga = new GameAPI(g);
                    gamesApi.Add(ga);
                }

                return StatusCode(200, new ResultData { Data = games, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna una partita dato il suo id
        /// </summary>
        /// <returns></returns>
        [Route("GetById")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GameManager gameManager = new GameManager(_connectionString);
                Games game = await gameManager.GetById(id);
                GameAPI ga = new GameAPI(game);

                // Recupero la formazione della squadra di casa
                PresenceManager presenceManager = new PresenceManager(_connectionString);
                List<Presences> presences = await presencesManager.GetByIdRound(id, game.IdTeamHome);

                foreach (Presences p in presences)
                {
                    ga.PlayersHome.Add(new PlayerGameAPI(p));
                }

                // Recupero la formazione della squadra in trasferta
                List<Presences> presences = await presencesManager.GetByIdRound(id, game.IdTeamAway);

                foreach (Presences p in presences)
                {
                    ga.PlayersAway.Add(new PlayerGameAPI(p));
                }

                return StatusCode(200, new ResultData { Data = ga, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna tutte le partite dato l'id di una squadra
        /// </summary>
        /// <returns></returns>
        [Route("GetByIdTeam")]
        [HttpGet]
        public async Task<IActionResult> GetByIdTeam([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GameManager manager = new GameManager(_connectionString);
                List<Games> games = await manager.GetByIdTeam(id);
                
                List<GameAPI> ga = new List<GameAPI>();

                foreach (Games g in games)
                {
                    ga.Add(new GameAPI(g));
                }

                return StatusCode(200, new ResultData { Data = ga, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Inserisce una nuova partita
        /// </summary>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Games body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GameManager manager = new GameManager(_connectionString);
                Games game = await manager.Insert(body);
                if (game == null)
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante l'inserimento della partita." });

                return StatusCode(200, new ResultData { Data = game, Status = false, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Aggiorna una partita
        /// </summary>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Games body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GameManager manager = new GameManager(_connectionString);
                Games game = await manager.Update(body);
                if (game == null)
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante l'aggiornamento della partita." });

                return StatusCode(200, new ResultData { Data = game, Status = false, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
