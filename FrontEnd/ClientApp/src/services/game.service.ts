import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

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
}
