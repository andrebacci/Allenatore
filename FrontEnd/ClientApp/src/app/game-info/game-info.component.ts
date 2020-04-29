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

  selectedPlayer: number;

  idGame: number = 0;

  listPlayerHome: number[] = [20];

  constructor(private gameService: GameService, private playerService: PlayerService, private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.mode = params["mode"];
      this.idGame = params["id"];

      // Recupero la partita
      this.getGameById();      
    });
  }

  // Recupera la partita dato il suo id
  getGameById(): void {
    this.gameService.getById(this.idGame).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.game = resultData.data as Game;

        // Recupero i giocatori delle due squadre
        this.getPlayersByIdTeam(this.game.idTeamHome);
        this.getPlayersByIdTeam(this.game.idTeamAway);
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
        if (this.game.idTeamHome == idTeam) {
          this.playersHome = resultData.data as Player[];
        } else {
          this.playersAway = resultData.data as Player[];
        }
      } else {
        // Errore
      }
    })
  }

  onChange(event: any): void {
    var andre = this.selectedPlayer;
  }
}
