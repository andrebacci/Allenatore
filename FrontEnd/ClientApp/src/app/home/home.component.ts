import { Component } from '@angular/core';
import { RankingService } from 'src/services/ranking.service';
import { ResultData } from 'src/models/resultData';
import { Ranking } from 'src/models/ranking';
import { RoundService } from '../../services/round.service';
import { Round } from '../../models/round';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  ranking: Ranking[] = [];

  lastRound: Round = null;
  nextRound: Round = null;

  constructor(private rankingService: RankingService, private roundService: RoundService) {

  }
  
  ngOnInit(): void {
    this.getRanking();
    this.getLastRound(); 
  }

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

  getLastRound(): void {
    this.roundService.getLast().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.lastRound = resultData.data as Round;
      } else {
        // Errore
      }
    })
  }

  getNextRound(): void {

  }
}
