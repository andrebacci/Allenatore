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
    public static class GolUtility
    {
        /// <summary>
        /// Ritorna il numero di gol fatti da un giocatore in una partita
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Gols> GetPlayerGolsByIdGame(int idPlayer, int idGame)
        {
            try
            {
                GolController controller = new GolController();
                ObjectResult objectResult = controller.GetGolByIdPlayerIdGame(idPlayer, idGame).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                if (resultData.Data == null)
                    return null;

                return resultData.Data as List<Gols>;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
    }
}
