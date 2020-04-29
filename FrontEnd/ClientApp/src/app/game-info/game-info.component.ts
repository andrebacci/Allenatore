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

  }
}
