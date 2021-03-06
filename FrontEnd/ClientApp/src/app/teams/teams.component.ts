import { Component } from '@angular/core';
import { TeamService } from '../../services/team.service';
import { Team } from '../../models/team';
import { ResultData } from '../../models/resultData';
import { Router } from '@angular/router';
import Utility from '../../utility/utility';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
})

export class TeamsComponent {

  teams: Team[] = [];

  constructor(private teamService: TeamService, private router: Router) {

  }

  ngOnInit(): void {
    this.getAll();
  }

  // Inizializza la lista con tutte le squadre
  getAll(): void {
    this.teamService.getAll().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.teams = resultData.data as Team[];
      } else {
        // Errore
      }
    });
  }

  // Apre la pagina del singolo team
  detailTeam(id: number): void {
    Utility.redirect('/team/detail/' + id, this.router);
  }

  // Apre la pagina di modifica del team
  updateTeam(id: number): void {
    Utility.redirect('/team/update/' + id, this.router);
  }
}
