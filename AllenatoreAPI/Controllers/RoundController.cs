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
    public class RoundController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public RoundController()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }

        /// <summary>
        /// Ritorna tutte le partite
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                RoundManager manager = new RoundManager(_connectionString);
                List< Rounds> rounds = await manager.GetAll();               

                return StatusCode(200, new ResultData { Data = rounds, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna tutte le partite
        /// </summary>
        /// <returns></returns>
        [Route("GetById")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                RoundManager manager = new RoundManager(_connectionString);
                Rounds rounds = await manager.GetById(id);

                return StatusCode(200, new ResultData { Data = rounds, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna l'ultima giornata giocata
        /// </summary>
        /// <returns></returns>
        [Route("GetLast")]
        [HttpGet]
        public async Task<IActionResult> GetLast()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                RoundManager manager = new RoundManager(_connectionString);
                Rounds round = await manager.GetLast();

                RoundAPI roundAPI = new RoundAPI(round);
                
                // Recupero le partite della giornata
                GameManager gameManager = new GameManager(_connectionString);
                List<Games> games = await gameManager.GetByRound(round.Id);                

                foreach (Games g in games)
                {
                    roundAPI.Games.Add(new RoundItemAPI(g));
                }

                return StatusCode(200, new ResultData { Data = roundAPI, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la prossima giornata da giocare
        /// </summary>
        /// <returns></returns>
        [Route("GetNext")]
        [HttpGet]
        public async Task<IActionResult> GetNext()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                RoundManager manager = new RoundManager(_connectionString);
                Rounds round = await manager.GetNext();

                RoundAPI roundAPI = new RoundAPI(round);

                // Recupero le partite della giornata
                GameManager gameManager = new GameManager(_connectionString);
                List<Games> games = await gameManager.GetByRound(round.Id);

                foreach (Games g in games)
                {
                    roundAPI.Games.Add(new RoundItemAPI(g));
                }

                return StatusCode(200, new ResultData { Data = roundAPI, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la giornata dato il suo numero
        /// </summary>
        /// <returns></returns>
        [Route("GetByNumber")]
        [HttpGet]
        public async Task<IActionResult> GetByNumber([FromQuery] int number)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                RoundManager manager = new RoundManager(_connectionString);
                Rounds round = await manager.GetByNumber(number);

                RoundAPI roundAPI = new RoundAPI(round);

                // Recupero le partite della giornata
                GameManager gameManager = new GameManager(_connectionString);
                List<Games> games = await gameManager.GetByRound(round.Id);

                foreach (Games g in games)
                {
                    roundAPI.Games.Add(new RoundItemAPI(g));
                }

                return StatusCode(200, new ResultData { Data = roundAPI, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Inserisce una nuova giornata
        /// </summary>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Rounds body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                RoundManager manager = new RoundManager(_connectionString);
                Rounds round = await manager.Insert(body);
                if (round == null)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento di una giornata" });
                    
                return StatusCode(200, new ResultData { Data = round, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
