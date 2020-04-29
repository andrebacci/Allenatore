import { Component } from "@angular/core";
import { GameService } from "../../services/game.service";
import { Game } from "../../models/game";
import { Router, ActivatedRoute } from "@angular/router";
import { ResultData } from "../../models/resultData";
import { Player } from "../../models/player";
import { PlayerService } from "../../services/player.service";

@Component({
  selector: 'app-game-info',
  templateUrl: './game-info.component.html',
})

export class GameInfoComponent {

  sub: any;

  mode: string = "";

  game: Game = null;

  playersHome: Player[] = [];
  playersAway: Player[] = [];

  idGame: number = 0;

  constructor(private gameService: GameService, private playerService: PlayerService, private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.mode = params["mode"];
      this.idGame = params["id"];

      // Recupero la partita
      this.getTeamById();

      // Recupero i giocatori delle due squadre
      this.playersHome = this.getPlayersByIdTeam(this.game.idTeamHome);
      this.playersAway = this.getPlayersByIdTeam(this.game.idTeamAway);
    });
  }

  // Recupera la partita dato il suo id
  getTeamById(): void {
    this.gameService.getById(this.idGame).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.game = resultData.data as Game;
      } else {
        // Errore
      }
    });
  }

  // Recupero i giocatori dato l'id della squadra
  getPlayersByIdTeam(idTeam: number): any {
    this.playerService.getByTeamId(idTeam).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        return resultData.data as Player[];
      } else {
        // Errore
      }
    })
  }
}
