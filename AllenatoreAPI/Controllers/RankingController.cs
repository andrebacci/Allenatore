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
                    RankingAPI ranking = new RankingAPI();
                    ranking.Team = t.Name;

                    foreach (Games g in games)
                    {
                        // Squadra in casa
                        if (g.IdTeamHome == t.Id)
                        {
                            ranking.Games++;
                            ranking.GoalMade = g.GolTeamHome.GetValueOrDefault();
                            ranking.GoalConceded = g.GolTeamAway.GetValueOrDefault();

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
                            ranking.GoalMade = g.GolTeamAway.GetValueOrDefault();
                            ranking.GoalConceded = g.GolTeamHome.GetValueOrDefault();

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

                //// Recupero tutte le squadre
                //TeamManager teamManager = new TeamManager(_connectionString);
                //List<Teams> teams = await teamManager.GetAll();

                //// Recupero tutte le partite
                //GameManager gameManager = new GameManager(_connectionString);
                //List<Games> games = await gameManager.GetRangeHome(null, null);                

                //foreach (Teams t in teams)
                //{
                //    games = games.Where(x => x.IdTeamHome == t.Id).ToList();

                //    RankingAPI ranking = new RankingAPI();
                //    ranking.Team = t.Name;

                //    foreach (Games g in games)
                //    {
                //        if (g.IdTeamHome != t.Id)
                //            continue;

                //        ranking.Games++;
                //        ranking.GoalMade = g.GolTeamHome;
                //        ranking.GoalConceded = g.GolTeamAway;

                //        if (g.GolTeamHome > g.GolTeamAway)
                //        {
                //            ranking.Points += 3;
                //            ranking.Wins++;
                //        }
                //        else if (p.GolTeamHome == g.GolTeamAway)
                //        {
                //            ranking.Points++;
                //            ranking.Draws++;
                //        }
                //        else 
                //        {
                //            ranking.Losts++;
                //        }
                //    }

                //    rankings.Add(ranking);
                //}

                //rankings.OrderByDescending(x => x.Points);

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

                //// Recupero tutte le squadre
                //TeamManager teamManager = new TeamManager(_connectionString);
                //List<Teams> teams = await teamManager.GetAll();

                //// Recupero tutte le partite
                //GameManager gameManager = new GameManager(_connectionString);
                //List<Games> games = await gameManager.GetRangeHome(null, null);

                //foreach (Teams t in teams)
                //{
                //    games = games.Where(x => x.IdTeamAway == t.Id).ToList();

                //    RankingAPI ranking = new RankingAPI();
                //    ranking.Team = t.Name;

                //    foreach (Games g in games)
                //    {
                //        if (g.IdTeamHome != t.Id)
                //            continue;

                //        ranking.Games++;
                //        ranking.GoalMade = g.GolTeamAway;
                //        ranking.GoalConceded = g.GolTeamHome;

                //        if (g.GolTeamAway > g.GolTeamHome)
                //        {
                //            ranking.Points += 3;
                //            ranking.Wins++;
                //        }
                //        else if (g.GolTeamAway == g.GolTeamHome)
                //        {
                //            ranking.Points++;
                //            ranking.Draws++;
                //        }
                //        else
                //        {
                //            ranking.Losts++;
                //        }
                //    }

                //    rankings.Add(ranking);
                //}

                //rankings.OrderByDescending(x => x.Points);

                return StatusCode(200, new ResultData { Data = rankings, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}