import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";

@Injectable()
export class TeamService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  getAll() {
    return this.http.get(this.baseUrl + 'api/Team/Teams');
  }

  getById(id: any) {
    return this.http.get(this.baseUrl + 'api/Team/GetById', {
      params: {
        id: id
      }
    });
  }
}
