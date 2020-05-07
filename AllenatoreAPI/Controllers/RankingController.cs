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
    public class RankingController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public RankingController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }

        /// <summary>
        /// Ritorna la classifica
        /// </summary>
        /// <returns></returns>
        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int start, int end)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                List<RankingAPI> rankings = new List<RankingAPI>();

                // Recupero tutte le squadre
                TeamManager teamManager = new TeamManager(_connectionString);
                List<Teams> teams = await teamManager.GetAll();

                // Recupero tutte le partite
                GameManager gameManager = new GameManager(_connectionString);
                List<Games> games = await gameManager.GetRange(start, end);

                foreach (Teams t in teams)
                {
                    RankingAPI ranking = new RankingAPI
                    {
                        IdTeam = t.Id,
                        Team = t.Name
                    };

                    foreach (Games g in games)
                    {
                        // Controllo se la partita si è giocata
                        if (g.GolTeamHome == null || g.GolTeamAway == null)
                            continue;

                        // Squadra in casa
                        if (g.IdTeamHome == t.Id)
                        {
                            ranking.Games++;
                            ranking.GoalMade += g.GolTeamHome.GetValueOrDefault();
                            ranking.GoalConceded += g.GolTeamAway.GetValueOrDefault();

                            if (g.GolTeamHome > g.GolTeamAway)
                            {
                                ranking.Points += 3;
                                ranking.Wins++;
                            }
                            else if (g.GolTeamHome == g.GolTeamAway)
                            {
                                ranking.Points++;
                                ranking.Draws++;
                            }
                            else
                            {
                                ranking.Losts++;
                            }
                        }
                        // Squadra in trasferta
                        else if (g.IdTeamAway == t.Id)
                        {
                            ranking.Games++;
                            ranking.GoalMade += g.GolTeamAway.GetValueOrDefault();
                            ranking.GoalConceded += g.GolTeamHome.GetValueOrDefault();

                            if (g.GolTeamAway > g.GolTeamHome)
                            {
                                ranking.Points += 3;
                                ranking.Wins++;
                            }
                            else if (g.GolTeamAway == g.GolTeamHome)
                            {
                                ranking.Points++;
                                ranking.Draws++;
                            }
                            else
                            {
                                ranking.Losts++;
                            }
                        }
                        else
                        {
                            // Niente
                        }
                    }

                    rankings.Add(ranking);
                }

                rankings = rankings.OrderByDescending(x => x.Points).ToList();

                return StatusCode(200, new ResultData { Data = rankings, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica delle partite in casa
        /// </summary>
        /// <returns></returns>
        [Route("GetHome")]
        [HttpGet]
        public async Task<IActionResult> GetHome([FromQuery] int? start, int? end)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                List<RankingAPI> rankings = new List<RankingAPI>();

                // Recupero tutte le squadre
                TeamManager teamManager = new TeamManager(_connectionString);
                List<Teams> teams = await teamManager.GetAll();

                // Recupero tutte le partite
                GameManager gameManager = new GameManager(_connectionString);
                List<Games> games = await gameManager.GetRange(start, end);

                foreach (Teams t in teams)
                {
                    games = games.Where(x => x.IdTeamHome == t.Id).ToList();

                    RankingAPI ranking = new RankingAPI
                    {
                        IdTeam = t.Id,
                        Team = t.Name
                    };

                    foreach (Games g in games)
                    {
                        // Controllo se la partita si è giocata
                        if (g.GolTeamHome == null || g.GolTeamAway == null)
                            continue;

                        if (g.IdTeamHome != t.Id)
                            continue;

                        ranking.Games++;

                        if (g.GolTeamHome != null && g.GolTeamAway != null)
                        {
                            ranking.GoalMade += g.GolTeamHome.GetValueOrDefault();
                            ranking.GoalConceded += g.GolTeamAway.GetValueOrDefault();
                        }

                        if (g.GolTeamHome > g.GolTeamAway)
                        {
                            ranking.Points += 3;
                            ranking.Wins++;
                        }
                        else if (g.GolTeamHome == g.GolTeamAway)
                        {
                            ranking.Points++;
                            ranking.Draws++;
                        }
                        else
                        {
                            ranking.Losts++;
                        }
                    }

                    rankings.Add(ranking);
                }

                rankings.OrderByDescending(x => x.Points);

                return StatusCode(200, new ResultData { Data = rankings, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica delle partite in trasferta
        /// </summary>
        /// <returns></returns>
        [Route("GetAway")]
        [HttpGet]
        public async Task<IActionResult> GetAway([FromQuery] int? start, int? end)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                List<RankingAPI> rankings = new List<RankingAPI>();

                // Recupero tutte le squadre
                TeamManager teamManager = new TeamManager(_connectionString);
                List<Teams> teams = await teamManager.GetAll();

                // Recupero tutte le partite
                GameManager gameManager = new GameManager(_connectionString);
                List<Games> games = await gameManager.GetRange(start, end);

                foreach (Teams t in teams)
                {
                    games = games.Where(x => x.IdTeamAway == t.Id).ToList();

                    RankingAPI ranking = new RankingAPI
                    {
                        IdTeam = t.Id,
                        Team = t.Name
                    };

                    foreach (Games g in games)
                    {
                        // Controllo se la partita si è giocata
                        if (g.GolTeamHome == null || g.GolTeamAway == null)
                            continue;

                        if (g.IdTeamAway != t.Id)
                            continue;

                        ranking.Games++;

                        if (g.GolTeamHome != null && g.GolTeamAway != null)
                        {
                            ranking.GoalMade += g.GolTeamAway.GetValueOrDefault();
                            ranking.GoalConceded += g.GolTeamHome.GetValueOrDefault();
                        }

                        if (g.GolTeamAway > g.GolTeamHome)
                        {
                            ranking.Points += 3;
                            ranking.Wins++;
                        }
                        else if (g.GolTeamAway == g.GolTeamHome)
                        {
                            ranking.Points++;
                            ranking.Draws++;
                        }
                        else
                        {
                            ranking.Losts++;
                        }
                    }

                    rankings.Add(ranking);
                }

                rankings.OrderByDescending(x => x.Points);

                return StatusCode(200, new ResultData { Data = rankings, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica per una squadra
        /// </summary>
        /// <returns></returns>
        [Route("GetByIdTeam")]
        [HttpGet]
        public async Task<IActionResult> GetByIdTeam([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await Get(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                RankingAPI ranking = listRank.Where(x => x.IdTeam == id).FirstOrDefault();

                return StatusCode(200, new ResultData { Data = ranking, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica in casa per una squadra
        /// </summary>
        /// <returns></returns>
        [Route("GetHomeByIdTeam")]
        [HttpGet]
        public async Task<IActionResult> GetHomeByIdTeam([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await GetHome(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                RankingAPI ranking = listRank.Where(x => x.IdTeam == id).FirstOrDefault();

                return StatusCode(200, new ResultData { Data = ranking, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica in trasferta per una squadra
        /// </summary>
        /// <returns></returns>
        [Route("GetAwayByIdTeam")]
        [HttpGet]
        public async Task<IActionResult> GetAwayByIdTeam([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await GetAway(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                RankingAPI ranking = listRank.Where(x => x.IdTeam == id).FirstOrDefault();

                return StatusCode(200, new ResultData { Data = ranking, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica in base ai gol fatti
        /// </summary>
        /// <returns></returns>
        [Route("GetScoredGoals")]
        [HttpGet]
        public async Task<IActionResult> GetScoredGoals()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await Get(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                listRank = listRank.OrderByDescending(x => x.GoalMade).ToList();

                return StatusCode(200, new ResultData { Data = listRank, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica in base ai gol subiti
        /// </summary>
        /// <returns></returns>
        [Route("GetConcededGoals")]
        [HttpGet]
        public async Task<IActionResult> GetConcededGoals()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await Get(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                listRank = listRank.OrderByDescending(x => x.GoalConceded).ToList();

                return StatusCode(200, new ResultData { Data = listRank, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica in base ai gol fatti in casa
        /// </summary>
        /// <returns></returns>
        [Route("GetScoredGoalsHome")]
        [HttpGet]
        public async Task<IActionResult> GetScoredGoalsHome()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await GetHome(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                listRank = listRank.OrderByDescending(x => x.GoalMade).ToList();

                return StatusCode(200, new ResultData { Data = listRank, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica in base ai gol subiti in casa
        /// </summary>
        /// <returns></returns>
        [Route("GetConcededGoalsHome")]
        [HttpGet]
        public async Task<IActionResult> GetConcededGoalsHome()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await GetHome(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                listRank = listRank.OrderByDescending(x => x.GoalConceded).ToList();

                return StatusCode(200, new ResultData { Data = listRank, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica in base ai gol fatti in trasferta
        /// </summary>
        /// <returns></returns>
        [Route("GetScoredGoalsAway")]
        [HttpGet]
        public async Task<IActionResult> GetScoredGoalsAway()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await GetAway(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                listRank = listRank.OrderByDescending(x => x.GoalMade).ToList();

                return StatusCode(200, new ResultData { Data = listRank, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Ritorna la classifica in base ai gol subiti in trasferta
        /// </summary>
        /// <returns></returns>
        [Route("GetConcededGoalsAway")]
        [HttpGet]
        public async Task<IActionResult> GetConcededGoalsAway()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                ObjectResult objectResult = await GetAway(0, 0) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                List<RankingAPI> listRank = resultData.Data as List<RankingAPI>;

                listRank = listRank.OrderByDescending(x => x.GoalConceded).ToList();

                return StatusCode(200, new ResultData { Data = listRank, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}