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

    public class UtilityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public FeetController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }

        /// <summary>
        /// Importa le squadre
        /// </summary>
        /// <returns></returns>
        [Route("ImportTeams")]
        [HttpGet]
        public async Task<IActionResult> ImportTeams()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Importa i giocatori per una squadra
        /// </summary>
        /// <returns></returns>
        [Route("ImportPlayers")]
        [HttpGet]
        public async Task<IActionResult> ImportPlayers([FromQuery] string team)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
