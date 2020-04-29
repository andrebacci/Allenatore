import { Component } from "@angular/core";
import { GameService } from "../../services/game.service";
import { Game } from "../../models/game";
import { Router, ActivatedRoute } from "@angular/router";
import { ResultData } from "../../models/resultData";
import { Player } from "../../models/player";
import { PlayerService } from "../../services/player.service";

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
})

export class GameComponent {

  sub: any;

  mode: string = "";

  game: Game = null;

  playersHome: Player[] = [];
  playersAway: Player[] = [];

  idGame: number = 0;

  isReadOnly: boolean = false;

  teamHome: string = "";
  teamAway: string = "";
  golHome: number = 0;
  golAway: number = 0;
  round: number = 0;
  date: Date = null;
  moduleHome: string = "";
  moduleAway: string = "";

  constructor(private gameService: GameService, private route: ActivatedRoute, private playerService: PlayerService, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.mode = params["mode"];

      if (this.mode == "create") {
        this.isReadOnly = false;
      } else {
        this.idGame = params['id'];

        if (this.mode == "update") {
          this.isReadOnly = false;
        } else if (this.mode == "detail") {
          this.isReadOnly = true;
        }

        // Recupero le informazioni del giocatore
        this.getGameById(this.idGame);
      }
    });
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

  // Restituisce i giocatori di una squadra (in ordine alfabetico)
  getPlayersByIdTeam(id: number): void {
    this.playerService.getByTeamId(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        if (this.game.idTeamHome == id) {
          this.playersHome = resultData.data as Player[];
        } else {
          this.playersAway = resultData.data as Player[];
        }
      } else {
        // Errore
      }
    });
  }

  // Inizializza i campi della form
  initForm(): void {
    this.teamHome = this.game.teamHome;
    this.teamAway = this.game.teamAway;

    this.golHome = this.game.golTeamHome;
    this.golAway = this.game.golTeamAway;

    this.round = this.game.round;
    this.date = this.game.date;

    this.moduleHome = this.game.moduleHome;
    this.moduleAway = this.game.moduleAway;
  }

  // Salva le informazioni base della partita
  save(goInfo: boolean): void {
    if (this.game == null) {
      this.game = new Game();
    }

    // Popolare i dati della partita

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
}
