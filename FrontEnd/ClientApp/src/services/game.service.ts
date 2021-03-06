import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Game } from "../models/game";
import { GameInfo } from "../models/gameInfo";

@Injectable()
export class GameService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce tutte le partite per una determinata giornata
  getByRound(round: any) {
    return this.http.get(this.baseUrl + 'api/Game/GetByRound', {
      params: {
        round: round
      }
    });
  }

  // Restituisce una partita dato il suo id
  getById(id: any) {
    return this.http.get(this.baseUrl + 'api/Game/GetById', {
      params: {
        id: id
      }
    });
  }

  // Restituisce tutte le partite dato l'id di una squadra
  getByIdTeam(id: any) {
    return this.http.get(this.baseUrl + 'api/Game/GetByIdTeam', {
      params: {
        id: id
      }
    })
  }

  // Restituisce tutte le partite giocate da un giocatore
  getGamesByIdPlayer(idPlayer: any) {
    return this.http.get(this.baseUrl + 'api/Game/GetGamesByIdPlayer', {
      params: {
        idPlayer: idPlayer
      }
    });
  }

  // Restituisce la formazione di una squadra
  getFormationByIdTeamIdGame(idTeam: any, idGame: any) {
    return this.http.get(this.baseUrl + 'api/Game/GetFormationByIdTeamIdGame', {
      params: {
        idTeam: idTeam,
        idGame: idGame
      }
    });
  }

  // Inserisce una nuova partita
  insert(game: Game) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = {
      headers: headers
    };

    let body = JSON.stringify({
      idTeamHome: game.idTeamHome,
      idTeamAway: game.idTeamAway,
      golTeamHome: game.golTeamHome,
      golTeamAway: game.golTeamAway,
      round: game.roundNumber,
      moduleHome: game.moduleHome,
      moduleAway: game.moduleAway
    });

    return this.http.post(this.baseUrl + 'api/Game/Insert', body, options);
  }

  // Inserisce / Aggiorna i dati di una partita
  insertInfo(gameInfo: GameInfo) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = {
      headers: headers
    };

    let body = JSON.stringify({
      formationHome: gameInfo.formationHome
    });

    return this.http.post(this.baseUrl + 'api/Game/InsertInfo', body, options);
  }

  // Aggiorna una partita
  update(game: Game) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = {
      headers: headers
    };

    let body = JSON.stringify({
      idTeamHome: game.idTeamHome,
      idTeamAway: game.idTeamAway,
      golTeamHome: game.golTeamHome,
      golTeamAway: game.golTeamAway,
      round: game.roundNumber,
      moduleHome: game.moduleHome,
      moduleAway: game.moduleAway
    });

    return this.http.put(this.baseUrl + 'api/Game/Update', body, options);
  }
}
