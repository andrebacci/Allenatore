using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Utils
{
    public static class Converter
    {
        public static string FeetToString(int id)
        {
            switch (id)
            {
                case 1:
                    return "Sinistro";
                case 2:
                    return "Destro";
                default:
                    return string.Empty;
            }
        }

        public static string RoleToString(int id)
        {
            switch (id)
            {
                case 1:
                    return "Portiere";
                case 2:
                    return "Terzino Sinistro";
                case 7:
                    return "Esterno Destro";
                default:
                    return string.Empty;
            }
        }

        public static int ConvertCategory(string category)
        {
            switch (category)
            {
                case "Serie D":
                    return Constant.SERIE_D;
                case "Eccellenza":
                    return Constant.ECCELLENZA;
                case "Promozione":
                    return Constant.PROMOZIONE;
                case "Prima Categoria":
                    return Constant.PRIMA_CATEGORIA;
                case "Seconda Categoria":
                    return Constant.SECONDA_CATEGORIA;
                case "Terza Categoria":
                    return Constant.TERZA_CATEGORIA;
                default:
                    return 0;
            }
        }
    }
}
