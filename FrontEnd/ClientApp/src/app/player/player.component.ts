import { Component } from "@angular/core";
import { Player } from "../../models/player";
import { Game } from "../../models/game";
import { PlayerService } from "../../services/player.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ResultData } from "../../models/resultData";
import { TeamService } from "../../services/team.service";
import { Feet } from "../../models/feet";
import { FeetService } from "../../services/feet.service";
import { RoleService } from "../../services/role.service";
import { GameService } from "../../services/game.service";
import { Role } from "../../models/role";
import { Penalty } from "../../models/penalty";
import Utility from "../../utility/utility";

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
})

export class PlayerComponent {

  player: Player = null;

  games: Game[] = [];

  isReadOnly: boolean = false;

  idPlayer: number = -1;

  sub: any;

  mode: string = "";

  lastname: string = "";
  firstname: string = "";
  age: number = 0;
  team: string = "";
  lastteam: string = "";
  details: string = "";

  selectedFeet;
  selectedRole;
  selectedPenalty;

  feets: Feet[] = [];
  roles: Role[] = [];
  penalties: Penalty[] = [];

  nameTeams: string[] = [];

  playerFullName: string = "";

  constructor(private playerService: PlayerService, private gameService: GameService, private teamService: TeamService, private feetService: FeetService, private roleService: RoleService, private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.mode = params['mode'];

      // Inizializzo le variabili constanti
      this.initConstant();

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

      // Inizializzo la form in caso di nuovo giocatore
      if (this.mode == "create") {
        this.initFormCreate();
      }

      // Recupero le partite giocate dal giocatore
      this.getGames();
    });
  }

  // Inizializza le variabili constanti
  initConstant(): void {
    // Inizializzo roles
    this.roleService.getAll().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.roles = resultData.data as Role[];
      } else {
        // Errore
      }
    })

    // Inizializzo feets
    this.feetService.getAll().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.feets = resultData.data as Feet[];
      } else {
        // Errore
      }
    })

    // Inizializzo penalties
    this.penalties.push(new Penalty(1, 'Sì'));
    this.penalties.push(new Penalty(2, 'No'));
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

  // Inizializza le partite giocate dal giocatore
  getGames(): void {
    this.gameService.getGamesByIdPlayer(this.idPlayer).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.games = resultData.data as Game[];
      } else {
        // Errore
      }
    });
  }

  // Inizializzo i campi della form
  initForm(): void {
    this.lastname = this.player.lastname;
    this.firstname = this.player.firstname;
    this.age = this.player.age;
    this.lastteam = this.player.lastTeamString;
    this.details = this.player.details;
    this.team = this.player.team;

    this.selectedFeet = this.player.feetString;
    this.selectedRole = this.player.roleString;
    this.selectedPenalty = this.player.penaltyString;

    this.playerFullName = this.lastname.toUpperCase() + " " + this.firstname;
  }

  // Inizializzo i campi della form se sono in modalità create
  initFormCreate(): void {
    this.selectedFeet = 'Destro';
    this.selectedRole = 'Portiere';
    this.selectedPenalty = 'No';
  }

  // Passa dalla modalità "detail" alla modalità "update"
  update(): void {
    Utility.redirect('/player/update/' + this.player.id, this.router);
  }

  // Salva le modifiche
  save(): void {
    if (this.player == null)
      this.player = new Player();

    this.checkTeam();

    this.player.lastname = this.lastname;
    this.player.firstname = this.firstname;
    this.player.age = this.age;
    this.player.details = this.details;
    this.player.image = "";
    this.player.team = this.team;

    this.player.feet = this.convertFeetToNumber(this.selectedFeet);
    this.player.role = this.convertRoleToNumber(this.selectedRole);
    this.player.penalty = this.convertPenaltyToBool(this.selectedPenalty);

    if (this.mode == 'update') {
      this.playerService.update(this.player).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {
          let player = resultData.data as Player;
          Utility.redirect('/player/detail/' + player.id, this.router);
        } else {
          // Errore
        }
      })
    } else if (this.mode == 'create') {
      this.playerService.insert(this.player).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {
          let player = resultData.data as Player;
          Utility.redirect('/player/detail/' + player.id, this.router);
        } else {
          // Errore
        }
      });
    }
  }

  // Controllo che le squadre (attuale e provenienza) inserite siano corrette
  checkTeam(): void {

  }

  convertFeetToNumber(feet: string): number {
    if (feet == 'Sinistro')
      return 1;

    return 2;
  }

  convertRoleToNumber(role: string): number {
    var pos = this.roles.map(function (e) { return e.description; }).indexOf(role);
    return pos + 1;
  }

  convertPenaltyToBool(penalty: string): boolean {
    if (penalty == 'Sì')
      return true;

    return false;
  }
}
