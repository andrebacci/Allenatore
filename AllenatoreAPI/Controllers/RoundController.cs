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
        /// Ritorna tutte le partite per una data giornata
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
    }
}
