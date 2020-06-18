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
    public static class CategoryUtility
    {
        public static string GetCategoryName(int id)
        {
            try
            {
                CategoryController controller = new CategoryController();
                ObjectResult objectResult = controller.GetById(id).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                if (resultData.Data == null)
                    return string.Empty;

                Category category = resultData.Data as Category;

                if (string.IsNullOrEmpty(category.Round))
                    return category.Name;

                return $"{category.Name} - {category.Round}";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
    }
}
