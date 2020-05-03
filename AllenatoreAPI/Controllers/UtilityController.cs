using AllenatoreAPI.Models;
using AllenatoreAPI.Result;
using AllenatoreAPI.Utils;
using BusinessLogic;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
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

    public class UtilityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public UtilityController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _connectionString = _configuration.GetValue<string>("ConnectionString");
        }

        /// <summary>
        /// Importa le squadre
        /// </summary>
        /// <returns></returns>
        [Route("ImportTeams")]
        [HttpGet]
        public async Task<IActionResult> ImportTeams()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                string filepath = _configuration.GetValue<string>("TeamFile");

                // Controllo l'esistenza del file
                if (!System.IO.File.Exists(filepath))
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"File dei team non trovato." });

                // Svuoto la tabella
                //UtilityManager utilityManager = new UtilityManager(_connectionString);
                //await utilityManager.Truncate("Teams");

                FileInfo fi = new FileInfo(filepath);

                using (ExcelPackage excelPackage = new ExcelPackage(fi))
                {
                    TeamController controller = new TeamController();

                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        ExcelRange rowValues = worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column];

                        if (rowValues["A" + row].Value == null)
                            return StatusCode(200, new ResultData { Data = true, Status = true, FunctionName = functionName, Message = $"Ok." });

                        Teams team = new Teams
                        {
                            Name = rowValues["A" + row].Value.ToString(),
                            City = rowValues["B" + row].Value.ToString(),
                            Mister = rowValues["C" + row].Value.ToString(),
                            Category = rowValues["D" + row].Value.ToString(),
                            //Logo = rowValues["E" + row].Value.ToString()
                        };

                        ObjectResult objectResult = await controller.Insert(team) as ObjectResult;
                        ResultData resultData = objectResult.Value as ResultData;
                        if (resultData.Data == null)
                            return StatusCode(200, new ResultData { Data = false, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento della squadra {team.Name}." });
                    }
                }

                return StatusCode(200, new ResultData { Data = true, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Importa i giocatori per una squadra
        /// </summary>
        /// <returns></returns>
        [Route("ImportPlayers")]
        [HttpGet]
        public async Task<IActionResult> ImportPlayers([FromQuery] int idTeam)
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                // Recupero il team
                TeamController teamController = new TeamController();
                ObjectResult objectResult = await teamController.GetById(idTeam) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;

                TeamAPI team = resultData.Data as TeamAPI;

                string filepath = string.Concat(_configuration.GetValue<string>("TeamPlayers"), "\\Ciassetta.xlsx"); //, team.Name);

                // Controllo l'esistenza del file
                if (!System.IO.File.Exists(filepath))
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"File dei giocatori non trovato." }); 
                
                FileInfo fi = new FileInfo(filepath);

                using (ExcelPackage excelPackage = new ExcelPackage(fi))
                {
                    PlayerController playerController = new PlayerController();

                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        ExcelRange rowValues = worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column];

                        if (rowValues["A" + row].Value == null)
                            return StatusCode(200, new ResultData { Data = true, Status = true, FunctionName = functionName, Message = $"Ok." });

                        Players player = new Players
                        {
                            IdTeam = idTeam,
                            Lastname = rowValues["A" + row].Value.ToString()
                        };

                        if (rowValues["B" + row].Value != null)
                            player.Firstname = rowValues["B" + row].Value.ToString();
                        
                        if (rowValues["C" + row].Value != null)
                            player.Age = Convert.ToInt32(rowValues["C" + row].Value);
                        
                        if (rowValues["D" + row].Value != null)
                            player.Role = Convert.ToInt32(rowValues["D" + row].Value);
                        
                        if (rowValues["E" + row].Value != null)
                            player.Feet = Convert.ToInt32(rowValues["E" + row].Value);
                        
                        if (rowValues["F" + row].Value != null)
                            player.Penalty = Convert.ToBoolean(rowValues["F" + row].Value);
                        
                        if (rowValues["G" + row].Value != null)
                            player.Details = rowValues["G" + row].Value.ToString();
                        
                        objectResult = await playerController.Insert(player) as ObjectResult;
                        resultData = objectResult.Value as ResultData;
                        if (resultData.Data == null)
                            return StatusCode(200, new ResultData { Data = false, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento del giocatore." });                                               
                    }
                }

                return StatusCode(200, new ResultData { Data = true, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }

        /// <summary>
        /// Importa il calendario creando le giornate e le partite
        /// </summary>
        /// <returns></returns>
        [Route("ImportRounds")]
        [HttpGet]
        public async Task<IActionResult> ImportRounds()
        {
            string functionName = Utility.GetRealMethodFromAsyncMethod(MethodBase.GetCurrentMethod());

            try
            {
                string filepath = _configuration.GetValue<string>("TeamFile");

                // Controllo l'esistenza del file
                if (!System.IO.File.Exists(filepath))
                    return StatusCode(200, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"File dei team non trovato." });

                FileInfo fi = new FileInfo(filepath);

                using (ExcelPackage excelPackage = new ExcelPackage(fi))
                {
                    RoundController roundController = new RoundController();
                    GameController gameController = new GameController();

                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        ExcelRange rowValues = worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column];

                        if (rowValues["A" + row].Value == null)
                            return StatusCode(200, new ResultData { Data = true, Status = true, FunctionName = functionName, Message = $"Ok." });

                        // Inserisco la giornata
                        Rounds r = new Rounds 
                        {
                            Number = Convert.ToInt32(rowValues["A" + row].Value),
                            Date = DateTime.Parse(rowValues["F" + row].Value.ToString())
                        };

                        ObjectResult objectResult = await roundController.Insert(r) as ObjectResult;
                        ResultData resultData = objectResult.Value as ResultData;
                        if (resultData.Data == null)
                            return StatusCode(200, new ResultData { Data = false, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento della giornata." });

                        Rounds round = resultData.Data as Rounds;

                        // Inserisco la partita
                        Games g = new Games
                        {
                            IdTeamHome = Convert.ToInt32(rowValues["B" + row].Value),
                            IdTeamAway = Convert.ToInt32(rowValues["C" + row].Value),
                            GolTeamHome = Convert.ToInt32(rowValues["D" + row].Value),
                            GolTeamAway = Convert.ToInt32(rowValues["E" + row].Value),
                            Round = round.Id
                        };

                        objectResult = await gameController.Insert(g) as ObjectResult;
                        resultData = objectResult.Value as ResultData;
                        if (resultData.Data == null)
                            return StatusCode(200, new ResultData { Data = false, Status = false, FunctionName = functionName, Message = $"Errore durante l'inserimento della partita." });
                        
                        return StatusCode(200, new ResultData { Data = true, Status = true, FunctionName = functionName, Message = $"Ok." });
                    }
                }

                return StatusCode(200, new ResultData { Data = true, Status = true, FunctionName = functionName, Message = $"Ok." });
            }
            catch (Exception exc)
            {
                return StatusCode(500, new ResultData { Data = null, Status = false, FunctionName = functionName, Message = $"{exc.Message}" });
            }
        }
    }
}
