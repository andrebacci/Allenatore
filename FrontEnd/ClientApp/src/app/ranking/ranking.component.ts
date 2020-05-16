import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { RankingService } from "../../services/ranking.service";
import { Ranking } from "../../models/ranking";
import { ResultData } from "../../models/resultData";

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
})

export class RankingComponent {
  sub: any;

  mode: string = "";

  ranking: Ranking[] = [];

  constructor(private rankingService: RankingService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.mode = params['mode'];

      if (this.mode == 'all') {
        this.getRanking();
      } else if (this.mode == 'home') {
        this.getRankingHome();
      } else if (this.mode == 'away') {
        this.getRankingAway();
      } else if (this.mode == 'scoredGoals') {
        this.getRankingScoredGoals();
      } else if (this.mode == 'concededGoals') {
        this.getRankingConcededGoals();
      }
    });
  }

  // Inizializza la classifica generale
  getRanking(): void {
    this.rankingService.get(0, 0).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.ranking = resultData.data as Ranking[];
      } else {
        // Errore
      }
    });
  }

  // Inizializza la classifica in casa
  getRankingHome(): void {
    this.rankingService.getHome(0, 0).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.ranking = resultData.data as Ranking[];
      } else {
        // Errore
      }
    });
  }

  // Inizializza la classifica in trasferta
  getRankingAway(): void {
    this.rankingService.getAway(0, 0).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.ranking = resultData.data as Ranking[];
      } else {
        // Errore
      }
    });
  }

  // Inizializza la classifica dei gol fatti
  getRankingScoredGoals(): void {
    this.rankingService.getScoredGoals(0, 0).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.ranking = resultData.data as Ranking[];
      } else {
        // Errore
      }
    });
  }

  // Inizializza la classifica dei gol fatti
  getRankingConcededGoals(): void {
    this.rankingService.getConcededGoals(0, 0).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.ranking = resultData.data as Ranking[];
      } else {
        // Errore
      }
    });
  }
}
