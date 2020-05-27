import { Component } from "@angular/core";
import { GameService } from "../../services/game.service";
import { Round } from "../../models/round";
import { RoundService } from "../../services/round.service";
import { ResultData } from "../../models/resultData";

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
})

export class CalendarComponent {

  rounds: Round[];

  constructor(private roundService: RoundService) {

  }

  ngOnInit(): void {
    // Inizializzo la lista delle giornate
    this.getRounds();
  }

  // Recupera la lista delle giornate
  getRounds(): void {
    this.roundService.getAll().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.rounds = resultData.data as Round[];
      } else {
        // Errore
      }
    });
  }
}
