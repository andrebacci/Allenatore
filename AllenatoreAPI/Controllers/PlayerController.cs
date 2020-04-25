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
        /// Ritorna tutti i giocatori dato l'id di una squadra
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("GetByTeamId")]
        [HttpGet]
        public async Task<IActionResult> GetByTeamId([FromQuery] int id)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                PlayerManager manager = new PlayerManager(_connectionString);
                List<Players> players = await manager.GetByTeamId(id);

                List<PlayerAPI> playersAPI = new List<PlayerAPI>();

                TeamController controller = new TeamController();

                foreach (Players p in players)
                {
                    PlayerAPI pa = new PlayerAPI(p)
                    {
                        FeetString = Constant.FeetToString(p.Feet.GetValueOrDefault()),
                        RoleString = Constant.RoleToString(p.Role.GetValueOrDefault()),
                        LastTeamString = await TeamUtility.GetLastTeamNameAsync(controller, p.IdTeam.GetValueOrDefault(), p.LastTeam.GetValueOrDefault())
                    };

                    playersAPI.Add(pa);
                }
                
                return StatusCode(200, new ResultData { Data = playersAPI, Status = true, FunctionName = functionName, Message = $"Giocatori trovati." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
