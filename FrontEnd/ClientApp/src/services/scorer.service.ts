import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class ScorerService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce tutti gli elementi
  getRankingGols() {
    return this.http.get(this.baseUrl + 'api/Gol/GetRankingGols');
  }

  getScorerByIdTeamIdGame(idTeam: any, idGame: any) {
    return this.http.get(this.baseUrl + 'api/Gol/GetByIdTeamIdGame', {
      params: {
        idTeam: idTeam,
        idGame: idGame
      }
    });
  }
}
