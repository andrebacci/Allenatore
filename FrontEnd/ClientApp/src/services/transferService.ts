import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Transfer } from "../models/transfer";

@Injectable()
export class TransferService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce tutti i trasferimenti
  getAll() {
    return this.http.get(this.baseUrl + 'api/Transfer/GetAll');
  }

  // Restituisce tutti i trasferimenti di una squadra
  getByIdTeam(id: any) {
    return this.http.get(this.baseUrl + 'api/Transfer/GetByIdTeam', {
      params: {
        id: id
      }
    })
  }

  // Inserisce un nuovo element
  insert(transfer: Transfer) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = {
      headers: headers
    };

    if (transfer.idTeamNew == 0)
      transfer.idTeamNew = null;

    if (transfer.idTeamOld == 0)
      transfer.idTeamOld = null;

    let body = JSON.stringify({
      id: transfer.id,
      idTeamNew: transfer.idTeamNew,
      idTeamOld: transfer.idTeamOld,
      idPlayer: transfer.idPlayer,
      //date: transfer.date
    });

    return this.http.post(this.baseUrl + 'api/Transfer/Insert', body, options);
  }
}
