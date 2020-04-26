import { Component } from "@angular/core";
import { GameService } from "../../services/game.service";
import { Game } from "../../models/game";
import { Router, ActivatedRoute } from "@angular/router";
import { ResultData } from "../../models/resultData";

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
})

export class GameComponent {

  sub: any;

  mode: string = "";

  game: Game = null;

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

  constructor(private gameService: GameService, private route: ActivatedRoute, private router: Router) {

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
}
