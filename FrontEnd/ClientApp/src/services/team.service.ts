import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";

@Injectable()
export class TeamService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce tutti i team
  getAll() {
    return this.http.get(this.baseUrl + 'api/Team/Teams');
  }

  // Restituisce un team dato il suo id
  getById(id: any) {
    return this.http.get(this.baseUrl + 'api/Team/GetById', {
      params: {
        id: id
      }
    });
  }

  // Aggiunge una nuova squadra
  insert(team: Team) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    let body = JSON.stringify({
      id: team.id,
      name: team.name,
      city: team.city,
      category: team.category,
      mister: team.mister
    });

    return this.http.post(this.baseUrl + 'api/Team/Insert', body, options);
  }

  // Aggiorna una squadra esistente
  update(team: Team) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    let body = JSON.stringify({
      id: team.id,
      name: team.name,
      city: team.city,
      category: team.category,
      mister: team.mister
    });

    return this.http.put(this.baseUrl + 'api/Team/Insert', body, options);
  }
}
