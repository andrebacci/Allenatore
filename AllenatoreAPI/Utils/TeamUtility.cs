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
        public static string GetLastTeamName(int idTeam, int idLastTeam)
        {
            try
            {
                if (idTeam == 0)
                    return string.Empty;

                if (idLastTeam == 0 || idTeam == idLastTeam)
                    return "Confermato";

                TeamController controller = new TeamController();
                ObjectResult objectResult = controller.GetNameById(idTeam).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;

                return resultData.Data.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        public static string GetTeamName(int idTeam)
        {
            try
            {
                if (idTeam == 0)
                    return string.Empty;

                TeamController controller = new TeamController();
                ObjectResult objectResult = controller.GetNameById(idTeam).Result as ObjectResult;
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
