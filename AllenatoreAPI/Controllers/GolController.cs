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

        /// <summary>
        /// Ritorna i gol segnati da un giocatore
        /// </summary>
        /// <param name="idPlayer"></param>
        /// <returns></returns>
        [Route("GetByIdPlayer")]
        [HttpGet]
        public async Task<IActionResult> GetByIdPlayer([FromQuery] int idPlayer)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GolManager manager = new GolManager(_connectionString);
                List<Gols> gols = await manager.GetByIdPlayer(idPlayer);
                if (gols == null)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante la ricerca dei gol." });

                return StatusCode(200, new ResultData { Data = gols, Status = true, FunctionName = functionName, Message = $"Ok." });

            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna i gol di un giocatore in una partita
        /// </summary>
        /// <param name="idPlayer"></param>
        /// <param name="idGame"></param>
        /// <returns></returns>
        [Route("GetGolByIdPlayerIdGame")]
        [HttpGet]
        public async Task<IActionResult> GetGolByIdPlayerIdGame([FromQuery] int idPlayer, int idGame)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                GolManager manager = new GolManager(_connectionString);
                List<Gols> gols = await manager.GetGolByIdPlayerIdGame(idPlayer, idGame);
                if (gols == null)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante la ricerca dei gol." });

                return StatusCode(200, new ResultData { Data = gols, Status = true, FunctionName = functionName, Message = $"Ok." });

            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica dei marcatori
        /// </summary>
        /// <returns></returns>
        [Route("GetRankingGols")]
        [HttpGet]
        public async Task<IActionResult> GetRankingGols()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                List<ScorerAPI> scorers = new List<ScorerAPI>();

                GolManager manager = new GolManager(_connectionString);
                List<Gols> gols = await manager.GetAll();
                if (gols == null)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante la ricerca dei gol." });

                foreach (Gols g in gols)
                {
                    if (scorers.Count > 0)
                    {
                        ScorerAPI s = scorers.Where(x => x.IdPlayer == g.IdPlayer).First();
                        if (s != null)
                        {
                            s.Gols++;
                            continue;
                        }
                    }
                    else
                    {
                        ScorerAPI scorer = new ScorerAPI
                        {
                            IdPlayer = g.IdPlayer,
                            Firstname = "",
                            Lastname = "",
                            Fullname = PlayerUtility.GetFullname(g.IdPlayer),
                            Teamname = TeamUtility.GetTeamName(g.IdTeam),
                            Gols = 1
                        };

                        scorers.Add(scorer);
                    }
                }

                scorers = scorers.OrderByDescending(x => x.Gols).ToList();

                return StatusCode(200, new ResultData { Data = scorers, Status = true, FunctionName = functionName, Message = $"Ok." });

            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
