﻿using AllenatoreAPI.Controllers;
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

        // Ritorna il fullname di un giocatore dato il suo id
        public static string GetFullname(int id)
        {
            try
            {
                Players p = GetPlyaer(id);
                if (p == null)
                    return string.Empty;
                
                return p.Lastname + " " + p.Firstname;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        // Ritorna il dettaglio di quanto ha giocato un giocatore
        public static string GetInfoMinutes(int? timeIn, int? timeOut)
        {
            try
            {
                if (timeIn == null)
                    return "Non entrato";
                
                if (timeIn == 0)
                {
                    if (timeOut == 90)
                        return "Titolare";
                    
                    return $"Titolare - Sostituito al minuto {{timeOut.GetValueOrDefault()}}";
                }
                else
                {
                    if (timeOut.GetValueOrDefault() == 90)
                        return $"Subentrato - Entrato al minuto {{timeIn.GetValueOrDefault()}}";
                    
                    return $"Subentrato e Sostituito - Entrato al minuto {{timeIn.GetValueOrDefault()}} ed uscito al minuto {{timeOut.GetValueOrDefault()}}";
                }                
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
    }
}
