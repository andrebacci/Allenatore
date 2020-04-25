using AllenatoreAPI.Controllers;
using AllenatoreAPI.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Utils
{
    public static class TeamUtility
    {
        public static async Task<string> GetLastTeamNameAsync(TeamController controller, int idTeam, int idLastTeam)
        {
            try
            {
                if (idLastTeam == 0 || idTeam == 0)
                    return string.Empty;

                if (idTeam == idLastTeam)
                    return "Confermato";

                ObjectResult objectResult = await controller.GetNameById(idTeam) as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                return resultData.Data.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
    }
}
