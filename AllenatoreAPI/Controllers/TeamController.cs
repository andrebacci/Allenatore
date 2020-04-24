using AllenatoreAPI.InternalModels;
using BusinessLogic;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IConfigurationRoot _configuration;

        public TeamController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        // Metodo che aggiunge una nuova squadra
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] TeamInternal body)
        {
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

                TeamManager tm = new TeamManager(_configuration.GetValue<string>("ConnectionString"));
                await tm.Insert(team);
            }
            catch (Exception exc)
            {

            }

            return Ok();
        }
    }
}
