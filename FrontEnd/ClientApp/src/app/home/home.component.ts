import { Component } from '@angular/core';
import { RankingService } from 'src/services/ranking.service';
import { ResultData } from 'src/models/resultData';
import { Ranking } from 'src/models/ranking';
import { RoundService } from '../../services/round.service';
import { Round } from '../../models/round';

import Utility from '../../utility/utility';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  ranking: Ranking[] = [];

  lastRound: Round = null;
  nextRound: Round = null;

  constructor(private rankingService: RankingService, private roundService: RoundService, private router: Router) {

  }
  
  ngOnInit(): void {
    this.getRanking();
    this.getLastRound();
    this.getNextRound();
  }

  // Recupera la classifica
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

  // Recupera l'ultima giornata giocata
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

  // Recupera la prossima giornata
  getNextRound(): void {
    this.roundService.getNext().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.nextRound = resultData.data as Round;
      } else {
        // Errore
      }
    })
  }

  // Apre la pagina di dettaglio della partita
  detailGame(idGame: number): void {
    Utility.redirect('/game/detail/' + idGame, this.router);
  }
}
