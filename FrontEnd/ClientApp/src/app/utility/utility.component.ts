import { Component } from "@angular/core";
import { UtilityService } from "../../services/utility.service";
import { ResultData } from "src/models/resultData";
import { Team } from "../../models/team";
import { TeamService } from "../../services/team.service";

@Component({
  selector: 'app-utility',
  templateUrl: './utility.component.html',
})

export class UtilityComponent {

  teams: Team[] = [];

  selectedTeam;

  constructor(private utilityService: UtilityService, private teamService: TeamService) {

  }

  ngOnInit(): void {
    this.getTeams();
  }

  // Inizializzo la lista di squadre
  getTeams(): void {
    this.teamService.getAll().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.teams = resultData.data as Team[];
      } else {
        // Errore
      }
    })
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
