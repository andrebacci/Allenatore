using AllenatoreAPI.InternalModels;
using AllenatoreAPI.Result;
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
            string functionName = MethodBase.GetCurrentMethod().Name;

            try
            {
                TeamManager tm = new TeamManager(_connectionString);
                List<Team> teams = await tm.GetAll();

                return StatusCode(200, new ResultData { Data = teams, Status = true, FunctionName = functionName, Message = $"Method ended with success." });
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
        [Route("TeamById")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            string functionName = MethodBase.GetCurrentMethod().Name;

            try
            {
                TeamManager tm = new TeamManager(_connectionString);
                Team team = await tm.Get(id);

                TeamInternal ti = new TeamInternal(team);

                return StatusCode(200, new ResultData { Data = ti, Status = true, FunctionName = functionName, Message = $"Method ended with success." });
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
        public async Task<IActionResult> Insert([FromBody] TeamInternal body)
        {
            string functionName = MethodBase.GetCurrentMethod().Name;

            try
            {
                Team team = new Team
                {
                    Id = body.Id,
                    Name = body.Name,
                    City = body.City,
                    Logo = body.Logo,
                    Mister = body.Mister,
                    Category = body.Category
                };

                TeamManager tm = new TeamManager(_connectionString);
                Team addedTeam = await tm.Insert(team);
                if (addedTeam != null)
                    return StatusCode(200, new ResultData { Data = addedTeam, Status = true, FunctionName = functionName, Message = $"Method ended with success." });
                else
                    return StatusCode(200, new ResultData { Data = null, Status = true, FunctionName = functionName, Message = $"Error during insert team." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
