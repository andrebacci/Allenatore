using AllenatoreAPI.Controllers;
using AllenatoreAPI.Result;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Utils
{
    public static class PlayerUtility
    {
        // Ritorna il nome del giocatore dato il suo id
        public static string GetPlayerFullname(int idPlayer)
        {
            try
            {
                PlayerController controller = new PlayerController();
                ObjectResult objectResult = controller.GetFullNameById(idPlayer).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;

                return resultData.Data.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        // Ritorna il giocatore dato il suo id
        public static Players GetPlyaer(int id)
        {
            try
            {
                PlayerController controller = new PlayerController();
                ObjectResult objectResult = controller.GetById(id).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;

                return resultData.Data as Players;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
    }
}
