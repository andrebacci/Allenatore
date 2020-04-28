import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class UtilityService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Importa le squadre
  importTeams() {
      this.http.get(this.baseUrl + '/api/Utility/ImportTeams');
  }

  // Importa i giocatori di una squadra
  importPlayers(id: any) {
      this.http.get(this.baseUrl + '/api/Utility/ImportPlayers', {
          params: {
              id: id
          }
      })
  }

  // Importa il calendario
  importRounds() {
      this.http.get(this.baseUrl + '/api/Utility/Rounds');
  }
}
