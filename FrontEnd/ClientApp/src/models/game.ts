import { PlayerGame } from "./playerGame";

export class Game {
  id: number = 0;

  idTeamHome: number;
  idTeamAway: number;

  golTeamHome: number = 0;
  golTeamAway: number = 0;

  date: Date = null;

  roundNumber: number = 0;

  moduleHome: string = "";
  moduleAway: string = "";

  teamHome: string = "";
  teamAway: string = "";

  playersHome: PlayerGame[] = [];
  playersAway: PlayerGame[] = [];
}
