import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Player } from "../models/player";

@Injectable()
export class PlayerService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce tutti i giocatori
  getAll() {
    return this.http.get(this.baseUrl + 'api/Player/GetAll');
  }

  // Restituisce tutti i giocatori dato l'id di una squadra
  getByTeamId(id: any) {
    return this.http.get(this.baseUrl + 'api/Player/GetByTeamId', {
      params: {
        id: id
      }
    });
  }

  // Restituisce il giocatore dato il suo id
  getById(id: any) {
    return this.http.get(this.baseUrl + 'api/Player/GetById', {
      params: {
        id: id
      }
    })
  }

  // Aggiunge un nuovo giocatore
  insert(player: Player) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = {
      headers: headers
    };

    let body = JSON.stringify({
      id: player.id,
      idTeam: player.idTeam,
      lastname: player.lastname,
      firstname: player.firstname,
      age: +player.age,
      role: player.role,
      feet: player.feet,
      lastteam: player.lastTeam,
      penalty: player.penalty,
      details: player.details,
      image: player.image
    });

    return this.http.post(this.baseUrl + 'api/Player/Insert', body, options);
  }

  // Aggiorna un giocatore esistente
  update(player: Player) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = {
      headers: headers
    };

    let body = JSON.stringify({
      id: player.id,
      idTeam: player.idTeam,
      lastname: player.lastname,
      firstname: player.firstname,
      age: player.age,
      role: player.role,
      feet: player.feet,
      lastteam: player.lastname,
      penalty: player.penalty,
      details: player.details,
      image: player.image
    });

    return this.http.put(this.baseUrl + 'api/Player/Update', body, options);
  }
}
