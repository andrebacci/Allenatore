using AllenatoreAPI.Controllers;
using AllenatoreAPI.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Utils
{
    public static class RoleUtility
    {
        public static string GetRoleDescription(int id)
        {
            try
            {
                if (id == 0)
                    return string.Empty;

                RoleController controller = new RoleController();
                ObjectResult objectResult = controller.GetDescriptionById(id).Result as ObjectResult;
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
