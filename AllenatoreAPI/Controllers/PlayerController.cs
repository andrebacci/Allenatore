using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        /// Inserisce un nuovo record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Insert([FromBody] PlayerInternal body)
        {
            try
            {

            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }      

        // Metodo che aggiorna un giocatore già esistente

        // Metodo che restituisce le statistiche per un giocatore
    }
}
