using AllenatoreAPI.Controllers;
using AllenatoreAPI.Models;
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

        // Ritorna il numero delle presenze di un giocatore
        public static int GetCountPresences(int id)
        {
            try
            {
                PresenceController presenceController = new PresenceController();
                ObjectResult objectResult = presenceController.GetPlayedByIdPlayer(id).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                if (resultData.Data == null)
                    return 0;

                return (resultData.Data as List<Presences>).Count;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        // Ritorna il numero di gol di un giocatore
        public static int GetCountGols(int id)
        {
            try
            {
                GolController golController = new GolController();
                ObjectResult objectResult = golController.GetByIdPlayer(id).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                if (resultData.Data == null)
                    return 0;

                return (resultData.Data as List<Gols>).Count;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        // Ritorna la lista di giocatori di una partita
        public static List<PlayerGameAPI> GetFormation(int idGame, int idTeam)
        {
            try
            {
                List<PlayerGameAPI> formation = new List<PlayerGameAPI>();

                PresenceController presenceController = new PresenceController();
                ObjectResult objectResult = presenceController.GetByIdGameIdTeam(idGame, idTeam).Result as ObjectResult;
                ResultData resultData = objectResult.Value as ResultData;
                if (resultData.Data == null)
                    return formation;

                List<Presences> presences = resultData.Data as List<Presences>;

                foreach (Presences p in presences)
                {
                    int? golsCount = null;
                    List<Gols> gols = GolUtility.GetPlayerGolsByIdGame(p.IdPlayer, p.IdGame);
                    if (gols != null)
                        golsCount = gols.Count;

                    PlayerGameAPI pg = new PlayerGameAPI
                    {
                        Id = p.IdPlayer,
                        Fullname = GetFullname(p.IdPlayer),
                        ChangeIn = p.TimeIn > 0 ? p.TimeIn : null,
                        ChangeOut = p.TimeOut < 90 ? p.TimeOut : null,
                        Number = p.Number.GetValueOrDefault(),
                        Gols = golsCount == 0 ? null : golsCount
                    };

                    formation.Add(pg);
                }

                formation = formation.OrderBy(x => x.Number).ToList();

                return formation;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
    }
}
