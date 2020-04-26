import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class FeetService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce tutti gli elementi
  getAll() {
    return this.http.get(this.baseUrl + 'api/Feet/GetAll');
  }
}
