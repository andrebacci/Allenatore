import { Component } from '@angular/core';
import { TeamService } from '../../services/team.service';
import { Team } from '../../models/team';
import { ResultData } from '../../models/resultData';
import { ActivatedRoute, Router } from '@angular/router';
import { Player } from '../../models/player';
import { PlayerService } from '../../services/player.service';

import Utility from '../../utility/utility';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
})

export class TeamComponent {

  team: Team = null;
  players: Player[] = [];

  idTeam: number = -1;

  sub: any;

  mode: string = "";

  teamName: string = "";
  teamCity: string = "";
  teamCategory: string = "";
  teamMister: string = "";

  isReadOnly: boolean = false;

  constructor(private teamService: TeamService, private playerService: PlayerService, private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {      
      this.mode = params['mode'];

      if (this.mode == "create") {
        this.isReadOnly = false;
      } else {
        this.idTeam = params['id'];

        if (this.mode == "update") {
          this.isReadOnly = false;          
        } else if (this.mode == "detail") {
          this.isReadOnly = true;          
        }

        // Recupero le informazioni della squadra
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

        // Recupero la rosa della squadra
        this.getPlayers();
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

  // Svuota tutti i campi
  cleanForm(): void {
    this.teamName = "";
    this.teamCity = "";
    this.teamCategory = "";
    this.teamMister = "";
  }

  // Salva le modifiche
  save(createAfter: boolean): void {
    if (this.team == null)
      this.team = new Team();

    this.team.name = this.teamName;
    this.team.city = this.teamCity;
    this.team.category = this.teamCategory;
    this.team.mister = this.teamMister;

    if (this.mode == "update") {
      this.teamService.update(this.team).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {
          let team = resultData.data as Team;
          Utility.redirect('/team/detail/' + team.id, this.router);
        } else {
          // Errore
        }
      })
    } else if (this.mode == "create") {
      this.teamService.insert(this.team).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {
          let team = resultData.data as Team;

          if (createAfter) {
            this.cleanForm();
          } else {
            Utility.redirect('/team/detail/' + team.id, this.router);
          }          
        }
      })
    } else {
      // Errore
    }
  }

  // Annulla le modifiche fatte
  undo(): void {
    if (this.mode == "update") {
      this.initForm();
    } else if (this.mode == "create") {
      this.cleanForm();
    }
  }

  // Passa dalla modalità "detail" alla modalità "update"
  update(): void {
    Utility.redirect('/team/update/' + this.team.id, this.router);
  }

  // Recupero la rosa della squadra
  getPlayers(): void {
    this.playerService.getByTeamId(this.team.id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.players = resultData.data as Player[];
      } else {
        // Errore
      }
    });
  }

  // Apre la pagina di dettaglio del giocatore
  detailPlayer(id: number): void {
    Utility.redirect('/player/detail/' + id, this.router);
  }
}
