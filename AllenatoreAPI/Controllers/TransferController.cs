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
    public class TransferController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public TransferController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }

        /// <summary>
        /// Ritorna tutti i trasferimenti
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TransferManager manager = new TransferManager(_connectionString);
                List<Transfers> transfers = await manager.GetAll();

                List<TransferAPI> ta = new List<TransferAPI>();

                foreach (Transfers t in transfers)
                {
                    ta.Add(new TransferAPI(t));
                }

                return StatusCode(200, new ResultData { Data = ta, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna i trasferimenti di una squadra
        /// </summary>
        /// <returns></returns>
        [Route("GetByIdTeam")]
        [HttpGet]
        public async Task<IActionResult> GetByIdTeam([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TransferManager manager = new TransferManager(_connectionString);
                List<Transfers> transfers = await manager.GetByIdTeam(id);

                List<TransferAPI> ta = new List<TransferAPI>();

                foreach (Transfers t in transfers)
                {
                    ta.Add(new TransferAPI(t));
                }

                return StatusCode(200, new ResultData { Data = ta, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Aggiunge un nuovo trasferimento
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Transfers body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TransferManager manager = new TransferManager(_connectionString);
                Transfers transfer = await manager.Insert(body);
                if (transfer != null)
                {
                    // Aggiorno il giocatore
                    Players player = PlayerUtility.GetPlyaer(body.IdPlayer);
                    player.LastTeam = body.IdTeamOld;
                    player.IdTeam = body.IdTeamNew;

                    PlayerManager plyerManager = new PlayerManager(_connectionString);
                    Players p = await plyerManager.Update(player);
                    if (p == null)
                        return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento del trasferimento." });

                    return StatusCode(200, new ResultData { Data = transfer, Status = true, FunctionName = functionName, Message = $"Ok." });
                }

                return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento del trasferimento." });                
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
