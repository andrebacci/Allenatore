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
                GameManager manager = new GameManager(_connectionString);
                Games game = await manager.GetById(id);
                GameAPI ga = new GameAPI(game);

                return StatusCode(200, new ResultData { Data = ga, Status = true, FunctionName = functionName, Message = $"Ok" });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
