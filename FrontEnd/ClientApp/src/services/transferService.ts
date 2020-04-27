import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Transfer } from "../models/transfer";

@Injectable()
export class TransferService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce tutti gli elementi
  getAll() {
    return this.http.get(this.baseUrl + 'api/Transfer/GetAll');
  }

  // Inserisce un nuovo element
  insert(transfer: Transfer) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = {
      headers: headers
    };

    let body = JSON.stringify({
      id: transfer.id,
      idTeamNew: transfer.idTeamNew,
      idTeamOld: transfer.idTeamOld,
      idPlayer: transfer.idPlayer
    });

    return this.http.post(this.baseUrl + 'api/Transfer/Insert', body, options);
  }
}
