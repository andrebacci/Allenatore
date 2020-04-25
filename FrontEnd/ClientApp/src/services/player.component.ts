import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class PlayerService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

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
}
