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
    public class PlayerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public PlayerController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }

        /// <summary>
        /// Ritorna tutti i giocatori
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PlayerManager manager = new PlayerManager(_connectionString);
                List<Players> players = await manager.GetAll();

                List<PlayerAPI> playersAPI = new List<PlayerAPI>();

                foreach (Players p in players)
                {
                    PlayerAPI pa = new PlayerAPI(p);
                    playersAPI.Add(pa);
                }

                return StatusCode(200, new ResultData { Data = playersAPI, Status = true, FunctionName = functionName, Message = $"Giocatori trovati." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna tutti i giocatori dato l'id di una squadra
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("GetByTeamId")]
        [HttpGet]
        public async Task<IActionResult> GetByTeamId([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PlayerManager manager = new PlayerManager(_connectionString);
                List<Players> players = await manager.GetByTeamId(id);

                List<PlayerAPI> playersAPI = new List<PlayerAPI>();

                TeamController controller = new TeamController();

                foreach (Players p in players)
                {
                    PlayerAPI pa = new PlayerAPI(p);
                    playersAPI.Add(pa);
                }

                return StatusCode(200, new ResultData { Data = playersAPI, Status = true, FunctionName = functionName, Message = $"Giocatori trovati." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna un giocatore dato il suo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetById")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PlayerManager manager = new PlayerManager(_connectionString);
                Players player = await manager.GetById(id);
                if (player == null)
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Giocatore non trovato." });

                PlayerAPI pa = new PlayerAPI(player);
                return StatusCode(200, new ResultData { Data = pa, Status = true, FunctionName = functionName, Message = $"Giocatore trovato con successo." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna cognome e nome di un giocatore dato il suo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetNameById")]
        [HttpGet]
        public async Task<IActionResult> GetFullNameById([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PlayerManager manager = new PlayerManager(_connectionString);
                string name = await manager.GetNameById(id);

                return StatusCode(200, new ResultData { Data = name, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna le statistiche di un giocatore
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetStatistics")]
        [HttpGet]
        public async Task<IActionResult> GetStatistics([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PresenceManager presenceManager = new PresenceManager(_connectionString);
                GolManager golManager = new GolManager(_connectionString);

                // Recupero le presenze
                List<Presences> presences = await presenceManager.GetPlayedByIdPlayer(id);

                // Recupero i gol
                List<Gols> gols = await golManager.GetByIdPlayer(id);

                int minute = 0;
                foreach (Presences p in presences)
                {
                    minute += (p.TimeOut.GetValueOrDefault() - p.TimeIn.GetValueOrDefault());
                }

                DateTime? lastGame = null;
                DateTime? lastGameHolder = null;

                if (presences.Count > 0)
                {
                    GameUtility.GetDate(presences.OrderByDescending(x => x.IdGame).FirstOrDefault().IdGame);
                    GameUtility.GetDate(presences.Where(x => x.TimeIn == 0).OrderByDescending(x => x.IdGame).FirstOrDefault().IdGame);
                }

                PlayerStatistics playerStatistics = new PlayerStatistics
                {
                    Minutes = minute,
                    GamesAll = presences.Count,
                    GamesHolder = presences.Where(x => x.TimeIn == 0).Count(),
                    GamesOut = presences.Where(x => x.TimeOut < 90).Count(),
                    GamesIn = presences.Where(x => x.TimeIn > 0).Count(),
                    Gols = gols.Count,
                    LastGame = lastGame,
                    LastGameHolder = lastGameHolder
                };

                return StatusCode(200, new ResultData { Data = playerStatistics, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Aggiunge un nuovo giocatore
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Players body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                if (body.IdTeam == 0)
                    body.IdTeam = null;

                if (body.LastTeam == 0)
                    body.LastTeam = null;

                PlayerManager manager = new PlayerManager(_connectionString);
                Players player = await manager.Insert(body);
                if (player == null)
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante l'inserimento del giocatore." });

                PlayerAPI pa = new PlayerAPI(player);
                return StatusCode(200, new ResultData { Data = pa, Status = true, FunctionName = functionName, Message = $"Giocatore inserito con successo." });

            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Aggiorna un giocatore esistente
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Players body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PlayerManager manager = new PlayerManager(_connectionString);
                Players player = await manager.Update(body);
                if (player == null)
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante l'aggiornamento del giocatore." });

                PlayerAPI pa = new PlayerAPI(player);
                return StatusCode(200, new ResultData { Data = pa, Status = true, FunctionName = functionName, Message = $"Giocatore aggiornato con successo." });

            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
