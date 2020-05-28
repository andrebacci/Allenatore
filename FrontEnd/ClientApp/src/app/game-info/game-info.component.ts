import { Component } from "@angular/core";
import { GameService } from "../../services/game.service";
import { Game } from "../../models/game";
import { Router, ActivatedRoute } from "@angular/router";
import { ResultData } from "../../models/resultData";
import { Player } from "../../models/player";
import { PlayerService } from "../../services/player.service";
import { PlayerGame } from "../../models/playerGame";
import Utility from "../../utility/utility";
import { GameInfo } from "../../models/gameInfo";

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

  formationHome: Player[] = [];
  formationAway: Player[] = [];

  scorerPlayers: any = [];

  idGame: number = 0;

  numbers;

  constructor(private gameService: GameService, private playerService: PlayerService, private route: ActivatedRoute, private router: Router) {   {
    }
    this.numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];
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
        this.getPlayersByIdTeam(this.game.idTeamAway);        {
          }
      } else {
        // Errore
      }
    });
  }

  // Recupero i giocatori dato l'id della squadra
  getPlayersByIdTeam(idTeam: number): void {
    this.playerService.getByTeamId(idTeam).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        if (this.game.idTeamHome == idTeam) {
          this.playersHome = resultData.data as Player[];

          // Recupero la formazione della squadra di casa
          this.getFormationByIdTeamIdGame(this.game.idTeamHome, this.game.id);
        } else {
          this.playersAway = resultData.data as Player[];

          // Recupero la formazione della squadra in trasferta
          this.getFormationByIdTeamIdGame(this.game.idTeamAway, this.game.id);
        }
      } else {
        // Errore
      }
    })
  }

  // Recupero le formazioni della squadra
  getFormationByIdTeamIdGame(idTeam: number, idGame: number): void {
    this.gameService.getFormationByIdTeamIdGame(idTeam, idGame).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        var formation = resultData.data as PlayerGame[];

        if (this.game.idTeamHome == idTeam) {
          for (var i = 0; i < formation.length; i++) {
            for (var j = 0; j < this.playersHome.length; j++) {
              if (this.playersHome[j] == null)
                continue;

              if (formation[i].id == this.playersHome[j].id) {
                this.formationHome[i] = this.playersHome[j];
              }
            }            
          }
        } else {
          for (var i = 0; i < formation.length; i++) {
            for (var j = 0; j < this.playersAway.length; j++) {
              if (this.playersAway[j] == null)
                continue;

              if (formation[i].id == this.playersAway[j].id) {
                this.formationAway[i] = this.playersAway[j];
              }
            }
          }          
        }
      }
    })
  }

  onChange(event: any, position: number): void {
    var andre = 0;
  }

  playerIsSelected(formation: Player[], idPlayer: number, position: number) {
    for (var i = 0; i < formation.length; i++) {
      if (formation[i].id == idPlayer) {
        if (position == i)
          return true;
      }
    }

    return false;
  }

  save(): void {
    // Creo l'oggetto da passare al back-end
    var gameInfo = new GameInfo();
    gameInfo.formationHome = this.formationHome;

    this.gameService.insertInfo(gameInfo).subscribe(res => {
      var result = res as ResultData;
      if (result.status) {

      } else {
        // Errore
      }
    })
  }

  undo(): void {
    Utility.redirect('/game/detail/' + this.idGame, this.router);
  }
}
