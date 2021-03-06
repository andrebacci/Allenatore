import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class RankingService {

  baseUrl: string = "https://localhost:44328/";

  constructor(private http: HttpClient) {

  }

  // Restituisce la classifica generale
  get(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/Get', {
      params: {
        start: start,
        end: end
      }
    });
  }

  // Restituisce la classifica in casa
  getHome(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/GetHome', {
      params: {
        start: start,
        end: end
      }
    })
  }

  // Restituisce la classifica in trasferta
  getAway(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/GetAway', {
      params: {
        start: start,
        end: end
      }
    })
  }

  // Restituisce la classifica dei gol fatti
  getScoredGoals(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/GetScoredGoals', {
      params: {
        start: start,
        end: end
      }
    })
  }

  // Restituisce la classifica dei gol fatti in casa
  getScoredGoalsHome(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/GetScoredGoalsHome', {
      params: {
        start: start,
        end: end
      }
    })
  }

  // Restituisce la classifica dei gol fatti in trasferta
  getScoredGoalsAway(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/GetScoredGoalsAway', {
      params: {
        start: start,
        end: end
      }
    })
  }

  // Restituisce la classifica dei gol subiti
  getConcededGoals(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/GetConcededGoals', {
      params: {
        start: start,
        end: end
      }
    })
  }

  // Restituisce la classifica dei gol subiti in casa
  getConcededGoalsHome(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/GetConcededGoalsHome', {
      params: {
        start: start,
        end: end
      }
    })
  }

  // Restituisce la classifica dei gol subiti in trasferta
  getConcededGoalsAway(start: any, end: any) {
    return this.http.get(this.baseUrl + 'api/Ranking/GetConcededGoalsAway', {
      params: {
        start: start,
        end: end
      }
    })
  }
}
