import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class RoundService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce l'ultima giornata
  getLast() {
    return this.http.get(this.baseUrl + 'api/Round/GetLast');
  }

  // Restituisce la prossima giornata
  getNext() {
    return this.http.get(this.baseUrl + 'api/Round/GetNext');
  }
}
