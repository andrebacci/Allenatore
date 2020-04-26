import { Component } from "@angular/core";
import { Player } from "../../models/player";
import { PlayerService } from "../../services/player.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ResultData } from "../../models/resultData";
import { TeamService } from "../../services/team.service";

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
})

export class PlayerComponent {

  player: Player = null;

  isReadOnly: boolean = false;

  idPlayer: number = -1;

  sub: any;

  mode: string = "";

  lastname: string = "";
  firstname: string = "";
  age: number = 0;
  team: string = "";
  lastteam: string = "";
  details: string = ""
  feet: number = 0;
  role: number = 0;
  penalty: string = "";

  feets: any = ["Destro", "Sinistro"];
  penalties: any = ["Sì", "No"];
  roles: any = ["Portiere", "Terzino Sinistro", "Difensore Centrale", "Terzino Destro"];

  nameTeams: string[] = [];

  constructor(private playerService: PlayerService, private teamService: TeamService, private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.mode = params['mode'];

      if (this.mode == "create") {
        this.isReadOnly = false;
      } else {
        this.idPlayer = params['id'];

        if (this.mode == "update") {
          this.isReadOnly = false;
        } else if (this.mode == "detail") {
          this.isReadOnly = true;
        }

        // Recupero le informazioni del giocatore
        this.getPlayerById(this.idPlayer);
      }

      // Recupero la lista dei nomi di tutte le squadre
      if (this.mode != "detail") {
        this.getNameTeams();
      }
    });
  }

  // Recupera la lista dei nomi delle squadre
  getNameTeams(): void {
    this.teamService.getNameTeams().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.nameTeams = resultData.data as string[];
      } else {
        // Errore
      }
    })
  }

  // Inizializzo il giocatore dato il suo id
  getPlayerById(id: number): void {
    this.playerService.getById(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.player = resultData.data as Player;

        // Inizializzo la form
        this.initForm();
      } else {
        // Errore
      }
    })
  }

  // Inizializzo i campi della form
  initForm(): void {
    this.lastname = this.player.lastname;
    this.firstname = this.player.firstname;
    this.age = this.player.age;
    this.feet = this.player.feetString;
    this.role = this.player.roleString;
    this.lastteam = this.player.lastTeamString;
    //this.penalty = this.player.penalty.toString();
    this.details = this.player.details;
    this.team = this.player.team;
  }

  // Redirect della pagina (DA SPOSTARE IN UNA UTILITY SE POSSIBILE)
  redirect(url: string): void {
    this.router.navigate([]).then(res => { window.open(url, '_self') });
  }

  // Passa dalla modalità "detail" alla modalità "update"
  update(): void {
    this.redirect('/player/update/' + this.player.id);
  }

  // Salva le modifiche
  save(): void {
    if (this.player == null)
      this.player = new Player();
    
    this.checkTeam();

    this.player.lastname = this.lastname;
    this.player.firstname = this.firstname;
    this.player.age = this.age;
    this.player.feet = this.convertFeetToNumber(this.feet);
    this.player.role = this.convertRoleToNumber(this.role);
    this.player.penalty = this.convertPenaltyToBool(this.penalty);
    this.player.details = this.details;
    this.player.image = "";
    this.player.team = this.team;

    if (this.mode == 'update') {
      this.playerService.update(this.player).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {
          let player = resultData.data as Player;
          this.redirect('/player/detail/' + player.id);
        } else {
          // Errore
        }
      })
    } else if (this.mode == 'create') {
      this.playerService.insert(this.player).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {
          let player = resultData.data as Player;
          this.redirect('/player/detail/' + player.id);
        } else {
          // Errore
        }
      });
    }
  }

  // Controllo che le squadre (attuale e provenienza) inserite siano corrette
  checkTeam(): void {
    //if (this.nameTeams.indexOf(this.team) > -1 && this.nameTeams.indexOf())
  }

  // Metodi di convert (DA SPOSTARE IN UNA CLASSE DI UTILITY)
  convertFeetToNumber(feetString: string): number {
    if (feetString == 'Sinistro')
      return 1;

    return 2;
  }

  convertRoleToNumber(roleString: string): number {
    // FARE UNO SWITCH
    if (roleString == 'Portiere')
      return 1;

    if (roleString == 'Terzino Sinistro')
      return 2;

    if (roleString == 'Difensore Centrale')
      return 3;
  }

  convertPenaltyToBool(penaltyString: string): boolean {
    if (penaltyString == 'Sì')
      return true;

    return false;
  }
}
