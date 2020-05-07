using AllenatoreAPI.Controllers;
using AllenatoreAPI.Result;
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
    }
}
