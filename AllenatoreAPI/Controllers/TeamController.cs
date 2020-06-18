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
    public class TeamController : ControllerBase
    {
        private readonly IConfigurationRoot _configuration;

        private readonly string _connectionString;

        public TeamController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }

        /// <summary>
        /// Ritorna tutte le squadre
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("Teams")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamManager tm = new TeamManager(_connectionString);
                List<Teams> teams = await tm.GetAll();
                if (teams == null || teams.Count == 0)
                {
                    return StatusCode(200, new ResultData { Data = teams, Status = true, FunctionName = functionName, Message = $"Nessuna squadra trovata." });
                }
                else
                {
                    //List<TeamAPI> teamAPI = new List<TeamAPI>();
                    //foreach (Team t in teams)
                    //{
                    //    teamAPI.Add(new TeamAPI(t));
                    //}

                    teams = teams.Where(x => Converter.ConvertCategory(x.Category) == _configuration.GetValue<int>("category")).ToList();

                    return StatusCode(200, new ResultData { Data = teams, Status = true, FunctionName = functionName, Message = $"Squadre trovate." });
                }
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la squadra dato il suo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetById")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamManager manager = new TeamManager(_connectionString);
                Teams team = await manager.Get(id);
                if (team == null)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Nessuna squadra trovata." });

                TeamAPI teamAPI = new TeamAPI(team);
                return StatusCode(200, new ResultData { Data = teamAPI, Status = true, FunctionName = functionName, Message = $"Squadra trovata con successo." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la squadra dato il suo nome
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetByName")]
        [HttpGet]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamManager manager = new TeamManager(_connectionString);
                Teams team = await manager.Get(name);
                if (team == null)
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Nessuna squadra trovata." });

                return StatusCode(200, new ResultData { Data = team, Status = true, FunctionName = functionName, Message = $"Squadra trovata con successo." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        [Route("GetNameById")]
        [HttpGet]
        public async Task<IActionResult> GetNameById([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamManager manager = new TeamManager(_connectionString);
                string name = await manager.GetNameById(id);

                return StatusCode(200, new ResultData { Data = name, Status = true, FunctionName = functionName, Message = $"Nome squadra trovato: {name}." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna una lista con i nomi di tutte le squadre
        /// </summary>
        /// <returns></returns>
        [Route("GetNameTeams")]
        [HttpGet]
        public async Task<IActionResult> GetNameTeams()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamManager manager = new TeamManager(_connectionString);
                List<string> names = await manager.GetNameTeams();

                return StatusCode(200, new ResultData { Data = names, Status = true, FunctionName = functionName, Message = $"Nomi trovati con successo." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }

        }

        /// <summary>
        /// Aggiunge una nuova squadra
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Teams body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamManager manager = new TeamManager(_connectionString);
                Teams team = await manager.Insert(body);
                if (team != null)
                    return StatusCode(200, new ResultData { Data = team, Status = true, FunctionName = functionName, Message = $"Squadra inserita correttamente." });

                return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento della squadra {body.Name}." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Aggiorna una squadra esistente
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Teams body)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamManager manager = new TeamManager(_connectionString);
                Teams team = await manager.Update(body);
                if (team != null)
                    return StatusCode(200, new ResultData { Data = team, Status = true, FunctionName = functionName, Message = $"Squadra aggiornata correttamente." });
                
                return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"Errore durante l'aggiornamento della squadra {body.Name}." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Restituisce le statistiche di una squadra dato il suo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetStatistics")]
        [HttpGet]
        public async Task<IActionResult> GetStatistics([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamStatisticsAPI statistics = new TeamStatisticsAPI();

                RankingController rankingController = new RankingController();
                ObjectResult objectResult = await rankingController.GetByIdTeam(id) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;

                RankingAPI ranking = resultData.Data as RankingAPI;

                statistics.Wins = ranking.Wins;
                statistics.Draws = ranking.Draws;
                statistics.Losts = ranking.Losts;
                statistics.ScoredGols = ranking.GoalMade;
                statistics.ConcededGols = ranking.GoalConceded;

                // Statistiche in casa
                objectResult = await rankingController.GetHomeByIdTeam(id) as ObjectResult;
                resultData = objectResult.Value as ResultData;

                ranking = resultData.Data as RankingAPI;

                statistics.WinsHome = ranking.Wins;
                statistics.DrawsHome = ranking.Draws;
                statistics.LostsHome = ranking.Losts;
                statistics.ScoredGolsHome = ranking.GoalMade;
                statistics.ConcededGolsHome = ranking.GoalConceded;

                // Statistiche in trasferta
                objectResult = await rankingController.GetAwayByIdTeam(id) as ObjectResult;
                resultData = objectResult.Value as ResultData;

                ranking = resultData.Data as RankingAPI;

                statistics.WinsAway = ranking.Wins;
                statistics.DrawsAway = ranking.Draws;
                statistics.LostsAway = ranking.Losts;
                statistics.ScoredGolsAway = ranking.GoalMade;
                statistics.ConcededGolsAway = ranking.GoalConceded;

                return StatusCode(200, new ResultData { Data = statistics, Status = true, FunctionName = functionName, Message = $"Statistiche trovate con successo." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Restituisce la storia delle posizioni in classifica della squadra
        /// </summary>
        /// <param name="idTeam"></param>
        /// <returns></returns>
        [Route("GetRankingHistory")]
        [HttpGet]
        public async Task<IActionResult> GetRankingHistory([FromQuery] int idTeam)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                List<int> rankingHistory = new List<int>();

                RankingController rankingController = new RankingController();

                RoundController roundController = new RoundController();
                ObjectResult objectResult = await roundController.GetAll() as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<Rounds> rounds = resultData.Data as List<Rounds>;

                foreach (Rounds r in rounds)
                {
                    objectResult = await rankingController.Get(r.Number.GetValueOrDefault(), r.Number.GetValueOrDefault()) as ObjectResult;
                    resultData = objectResult.Value as ResultData;
                    List<RankingAPI> ranks = resultData.Data as List<RankingAPI>;

                    RankingAPI rank = ranks.Where(x => x.IdTeam == idTeam).FirstOrDefault();
                    int index = ranks.IndexOf(rank);
                    rankingHistory.Add(index);
                }

                return StatusCode(200, new ResultData { Data = rankingHistory, Status = true, FunctionName = functionName, Message = $"Statistiche trovate con successo." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
