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

  selectedPlayers: any = [];

  idGame: number = 0;

  numbers;

  constructor(private gameService: GameService, private playerService: PlayerService, private route: ActivatedRoute, private router: Router) {
    //this.numbers = Array(5).fill().map((x,i)=>i);
    this.numbers = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20];
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

          for (var i = 0; i < this.playersHome.length; i++) {
            this.selectedPlayers[i] = this.playersHome[i].id;
          }

        } else {
          this.playersAway = resultData.data as Player[];
        }
      } else {
        // Errore
      }
    })
  }

  onChange(event: any): void {

  }
}
