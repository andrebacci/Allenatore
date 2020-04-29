import { Component } from "@angular/core";
import { GameService } from "../../services/game.service";
import { Game } from "../../models/game";
import { Router, ActivatedRoute } from "@angular/router";
import { ResultData } from "../../models/resultData";
import { Player } from "../../models/player";
import { PlayerService } from "../../services/player.service";
import { TeamService } from "../../services/team.service";
import { Team } from "../../models/team";
import Utility from "../../utility/utility";

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
})

export class GameComponent {

  sub: any;

  mode: string = "";

  game: Game = null;

  teams: Team[] = [];

  playersHome: Player[] = [];
  playersAway: Player[] = [];

  selectedTeamHome;
  selectedTeamAway;

  idGame: number = 0;

  isReadOnly: boolean = false;

  golHome: number = 0;
  golAway: number = 0;
  round: number = 0;
  date: Date = null;
  moduleHome: string = "";
  moduleAway: string = "";

  constructor(private gameService: GameService, private playerService: PlayerService, private teamService: TeamService, private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.mode = params["mode"];

      if (this.mode == "create") {
        this.isReadOnly = false;
        this.getTeams();
      } else {
        this.idGame = params['id'];

        if (this.mode == "update") {
          this.isReadOnly = false;          
        } else if (this.mode == "detail") {
          this.isReadOnly = true;
        }

        // Recupero le squadre
        this.getTeams();

        // Recupero le informazioni della partita
        this.getGameById(this.idGame);
      }
    });
  }

  // Restituisce tutte le squadre
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

  // Restituisce la partita dato il suo id
  getGameById(id: number): void {
    this.gameService.getById(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.game = resultData.data as Game;

        this.initForm();
      } else {
        // Errore
      }
    })
  }

  // Inizializza i campi della form
  initForm(): void {
    this.selectedTeamHome = this.game.teamHome;
    this.selectedTeamAway = this.game.teamAway;

    this.golHome = this.game.golTeamHome;
    this.golAway = this.game.golTeamAway;

    this.round = this.game.round;
    this.date = this.game.date;

    this.moduleHome = this.game.moduleHome;
    this.moduleAway = this.game.moduleAway;
  }

  // Salva le informazioni base della partita
  save(): void {
    if (this.game == null) {
      this.game = new Game();
    }

    // TODO: popolare i dati della partita

    if (this.mode == "update") {
      this.gameService.update(this.game).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {

        } else {
          // Errore
        }
      });
    } else if (this.mode == "create") {
      this.gameService.insert(this.game).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {

        } else {
          // Errore
        }
      });
    }
  }

  // Apre la pagina game-info in modalità update o edit
  info(): void {
    Utility.redirect('game-info/' + this.mode + '/' + this.idGame, this.router);
  }
}
