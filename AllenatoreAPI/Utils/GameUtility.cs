using AllenatoreAPI.Controllers;
using AllenatoreAPI.Models;
using AllenatoreAPI.Result;
using BusinessLogic;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Utils
{
    public static class GameUtility
    {
        /// <summary>
        /// Ritorna il numero della giornata
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetNumber(int id)
        {
            try
            {
                RoundController controller = new RoundController();
                ObjectResult objectResult = controller.GetById(id).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                if (resultData.Data == null)
                    return 0;

                return (resultData.Data as Rounds).Number.GetValueOrDefault();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        /// <summary>
        /// Ritorna la data della partita
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DateTime? GetDate(int id)
        {
            try
            {
                id = GetRoundId(id);

                RoundController controller = new RoundController();
                ObjectResult objectResult = controller.GetById(id).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                if (resultData.Data == null)
                    return null;

                return (resultData.Data as Rounds).Date;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        /// <summary>
        /// Ritorna l'id della giornata dato l'id della partita
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetRoundId(int id)
        {
            try
            {
                GameController controller = new GameController();
                ObjectResult objectResult = controller.GetById(id).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                if (resultData.Data == null)
                    return 0;

                return (resultData.Data as GameAPI).Round.GetValueOrDefault();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
    }
}
