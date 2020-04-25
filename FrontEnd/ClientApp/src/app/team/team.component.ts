import { Component } from '@angular/core';
import { TeamService } from '../../services/team.service';
import { Team } from '../../models/team';
import { ResultData } from '../../models/resultData';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
})

export class TeamComponent {

  team: Team = null;

  idTeam: number = -1;

  sub: any;

  mode: string = "";

  teamName: string = "";
  teamCity: string = "";
  teamCategory: string = "";
  teamMister: string = "";

  isReadOnly: boolean = false;

  constructor(private teamService: TeamService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {      
      this.mode = params['mode'];

      if (this.mode == "create") {
        this.isReadOnly = false;
      } else {
        if (this.mode == "update") {
          this.isReadOnly = false;
          this.idTeam = params['id'];
        } else if (this.mode == "detail") {
          this.isReadOnly = true;
          this.idTeam = params['id'];
        }

        // Recupero il team
        this.getTeamById(this.idTeam);
      }
    });
  }

  // Inizializza il team dato il suo id
  getTeamById(id: number): void {
    this.teamService.getById(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.team = resultData.data as Team;

        // Inizializzo la form una volta che ho il team
        this.initForm();
      } else {
        // Errore
      }
    });
  }

  // Inizializza i campi della form
  initForm(): void {
    this.teamName = this.team.name;
    this.teamCity = this.team.city;
    this.teamCategory = this.team.category;
    this.teamMister = this.team.mister;    
  }

  // Salva le modifiche
  save(): void {
    this.team.name = this.teamName;
    this.team.city = this.teamCity;
    this.team.category = this.teamCategory;
    this.team.mister = this.teamMister;

    if (this.mode == "update") {
      // UPDATE
    } else if (this.mode == "create") {
      // CREATE
    } else {
      // Errore
    }
  }
}
