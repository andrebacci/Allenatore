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

    public class GolController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public GolController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }       

        /// <summary>
        /// Inserisce una nuova presenza
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Gols body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GolManager manager = new GolManager(_connectionString);
                Gols gol = await manager.Insert(body);
                if (gol == null)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento di un gol." });

                return StatusCode(200, new ResultData { Data = gol, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
