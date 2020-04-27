import { Component } from '@angular/core';
import { RankingService } from 'src/services/ranking.service';
import { ResultData } from 'src/models/resultData';
import { Ranking } from 'src/models/ranking';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  ranking: Ranking[] = [];

  constructor(private rankingService: RankingService) {

  }
  
  ngOnInit(): void {
    this.getRanking();
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
}
