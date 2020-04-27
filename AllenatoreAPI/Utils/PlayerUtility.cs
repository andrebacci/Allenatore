using AllenatoreAPI.Controllers;
using AllenatoreAPI.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Utils
{
    public static class PlayerUtility
    {
        public static string GetPlayerName(int idPlayer)
        {
            try
            {
                PlayerController controller = new PlayerController();
                ObjectResult objectResult = controller.GetNameById(idPlayer).Result as ObjectResult;
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
