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
        /// Ritorna tutti i giocatori
        /// </summary>
        /// <returns></returns>
        [Route("GetByIdRound")]
        [HttpGet]
        public async Task<IActionResult> GetByIdRound([FromQuery] int idRound, int idTeam)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PresenceManager manager = new PresenceManager(_connectionString);
                List<Presences> presences = await manager.GetByIdRound(idRound, idTeam);

                return StatusCode(200, new ResultData { Data = presences, Status = true, FunctionName = functionName, Message = $"Piedi trovati." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
