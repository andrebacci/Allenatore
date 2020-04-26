import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class RankingService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce la classifica
  get() {
    return this.http.get(this.baseUrl + 'api/Ranking/Get');
  }
}
