import { Component } from "@angular/core";
import { UtilityService } from "src/services/utility.service";
import { ResultData } from "src/models/resultData";

@Component({
  selector: 'app-utility',
  templateUrl: './utility.component.html',
})

export class PlayerComponent {

  selectedTeam;

  constructor(private utilityService: UtilityService) {

  }

  ngOnInit(): void {

  }
  
  // Importa le squadre
  importTeams(): void {
    this.utilityService.importTeams().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        
      } else {
        // Errore
      }
    })
  }

  // Importa i giocatori di una squadra
  importPlayers(): void {
    this.utilityService.importPlayers(this.selectedTeam).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {

      } else {
        // Errore
      }
    })
  }

  // Importa le giornate
  importRounds(): void {
    this.utilityService.importRounds().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {

      } else {
        // Errore
      }
    })
  }
}
