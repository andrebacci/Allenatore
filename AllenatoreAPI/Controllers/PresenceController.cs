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

    public class PresenceController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public PresenceController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }

        /// <summary>
        /// Ritorna tutti i giocatori di una squadra che hanno giocato una partita
        /// </summary>
        /// <returns></returns>
        [Route("GetByIdGameIdTeam")]
        [HttpGet]
        public async Task<IActionResult> GetByIdGameIdTeam([FromQuery] int idGame, int idTeam)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PresenceManager manager = new PresenceManager(_connectionString);
                List<Presences> presences = await manager.GetByIdGameIdTeam(idGame, idTeam);

                return StatusCode(200, new ResultData { Data = presences, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna tutte le presenze di un giocatore
        /// </summary>
        /// <returns></returns>
        [Route("GetPlayedByIdPlayer")]
        [HttpGet]
        public async Task<IActionResult> GetPlayedByIdPlayer([FromQuery] int idPlayer)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PresenceManager manager = new PresenceManager(_connectionString);
                List<Presences> presences = await manager.GetPlayedByIdPlayer(idPlayer);

                return StatusCode(200, new ResultData { Data = presences, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Inserisce una nuova presenza
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Presences body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PresenceManager manager = new PresenceManager(_connectionString);
                Presences presences = await manager.Insert(body);
                if (presences == null)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento di una presenza." });

                return StatusCode(200, new ResultData { Data = presences, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Rimuove le presenze dei giocatori di una squadra in una partita
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int idGame, int idTeam)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PresenceManager manager = new PresenceManager(_connectionString);
                bool deleted = await manager.Delete(idGame, idTeam);
                if (!deleted)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante la cancellazione delle presenza della squadra {idTeam} nella partita {idGame}." });

                return StatusCode(200, new ResultData { Data = true, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
