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
                {
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Nessuna squadra trovata." });
                }
                else
                {
                    TeamAPI teamAPI = new TeamAPI(team);

                    return StatusCode(200, new ResultData { Data = teamAPI, Status = true, FunctionName = functionName, Message = $"Squadra trovata con successo." });
                }
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

                return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante l'inserimento della squadra {body.Name}." });
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
                else
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Errore durante l'aggiornamento della squadra {body.Name}." });
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
        [Route("Statistics")]
        [HttpGet]
        public async Task<IActionResult> GetStatistics([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                TeamStatisticsAPI statistics = new TeamStatisticsAPI();
                return StatusCode(500, new ResultData { Data = statistics, Status = true, FunctionName = functionName, Message = $"Statistiche trovate con successo." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
