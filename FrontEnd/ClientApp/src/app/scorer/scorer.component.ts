import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { RankingService } from "../../services/ranking.service";
import { Ranking } from "../../models/ranking";
import { ResultData } from "../../models/resultData";
import { Scorer } from "../../models/scorer";
import { ScorerService } from "../../services/scorer.service";

@Component({
  selector: 'app-scorer',
  templateUrl: './scorer.component.html',
})

export class ScorerComponent {

  scorers: Scorer[] = [];

  constructor(private scorerService: ScorerService) {

  }

  ngOnInit(): void {
    this.getScorers();
  }

  // Inizializzo i marcatori
  getScorers(): void {
    this.scorerService.getRankingGols().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.scorers = resultData.data as Scorer[];
      } else {
        // Errore
      }
    });
  }
}
