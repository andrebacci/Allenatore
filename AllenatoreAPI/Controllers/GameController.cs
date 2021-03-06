﻿using AllenatoreAPI.Models;
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
                    GameAPI ga = new GameAPI(g, false);
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
                GameAPI ga = new GameAPI(game, true);

                return StatusCode(200, new ResultData { Data = ga, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la formazione di una squadra
        /// </summary>
        /// <param name="idTeam"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("GetFormationByIdTeamIdGame")]
        [HttpGet]
        public async Task<IActionResult> GetFormationByIdTeamIdGame([FromQuery] int idTeam, int idGame)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                List<PlayerGameAPI> formation = PlayerUtility.GetFormation(idGame, idTeam);

                return StatusCode(200, new ResultData { Data = formation, Status = true, FunctionName = functionName, Message = $"Ok" });
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
                    ga.Add(new GameAPI(g, false));
                }

                return StatusCode(200, new ResultData { Data = ga, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna tutte le partite dato l'id della squadra in casa e l'id della squadra in trasferta
        /// </summary>
        /// <returns></returns>
        [Route("GetByIdTeams")]
        [HttpGet]
        public async Task<IActionResult> GetByIdTeams([FromQuery] int idTeamHome, int idTeamAway)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GameManager manager = new GameManager(_connectionString);
                Games games = await manager.GetByIdTeams(idTeamHome, idTeamAway);

                GameAPI ga = new GameAPI(games, false);

                return StatusCode(200, new ResultData { Data = ga, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Restituisce tutte le partite giocate da un giocatore
        /// </summary>
        /// <returns></returns>
        [Route("GetGamesByIdPlayer")]
        [HttpGet]
        public async Task<IActionResult> GetGamesByIdPlayer([FromQuery] int idPlayer)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                List<GamePlayerAPI> ga = new List<GamePlayerAPI>();

                // Recupero le presenze
                PresenceManager presenceManager = new PresenceManager(_connectionString);
                List<Presences> presences = await presenceManager.GetPlayedByIdPlayer(idPlayer);

                // Recupero la partita data la presenza                
                GameManager gameManager = new GameManager(_connectionString);

                foreach (Presences p in presences)
                {
                    Games game = await gameManager.GetById(p.IdGame);
                    GamePlayerAPI gp = new GamePlayerAPI(game)
                    {
                        TimeIn = p.TimeIn,
                        TimeOut = p.TimeOut,
                        Info = PlayerUtility.GetInfoMinutes(p.TimeIn, p.TimeOut)
                    };

                    ga.Add(gp);
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

        [Route("InsertInfo")]
        [HttpPost]
        public async Task<IActionResult> InsertInfo([FromBody] GameInfoAPI gameInfo)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                // Cancello le presenze della squadra di casa e della squadra in trasferta
                PresenceManager presenceManager = new PresenceManager(_connectionString);
                bool result = presenceManager.Delete(gameInfo.IdTeamHome, gameInfo.IdGame).Result;
                if (!result)
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante la cancellazione delle presenze di {gameInfo.IdTeamHome} in {gameInfo.IdGame}." });
                    
                result = presenceManager.Delete(gameInfo.IdTeamAway, gameInfo.IdGame).Result;
                if (!result)
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante la cancellazione delle presenze di {gameInfo.IdTeamAway} in {gameInfo.IdGame}." });

                // Aggiunto le presenze della squadra di casa e della squadra in trasferta
                int i = 0;
                int j = 0;

                foreach (PlayerAPI p in gameInfo.FormationHome)
                {
                    i++;

                    Presences pre = new Presences
                    {
                        IdPlayer = p.Id,
                        IdGame = gameInfo.IdGame,
                        Number = i
                    };

                    if (i < 12)
                        pre.TimeIn = 0;

                    Presences added = presenceManager.Insert(pre).Result;
                    if (added == null)
                        return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante l'inserimento della presenza {pre.IdPlayer} di {gameInfo.IdTeamAway} in {gameInfo.IdGame}." });
                }
                
                foreach (PlayerAPI p in gameInfo.FormationAway)
                {
                    j++;

                    Presences pre = new Presences
                    {
                        IdPlayer = p.Id,
                        IdGame = gameInfo.IdGame,
                        Number = i
                    };

                    if (i < 12)
                        pre.TimeIn = 0;

                    Presences added = presenceManager.Insert(pre).Result;
                    if (added == null)
                        return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante l'inserimento della presenza {pre.IdPlayer} di {gameInfo.IdTeamAway} in {gameInfo.IdGame}." });                                           
                }

                // TODO: finire --> marcatori e cambi

                return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Ok" });
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
