import { PlayerGame } from "./playerGame";

export class Game {
  id: number = 0;
  idTeamHome: number = 0;
  idTeamAway: number = 0;

  golTeamHome: number = 0;
  golTeamAway: number = 0;

  date: Date = null;

  round: number = 0;

  moduleHome: string = "";
  moduleAway: string = "";

  teamHome: string = "";
  teamAway: string = "";

  playersHome: PlayerGame[] = [];
  playersAway: PlayerGame[] = [];
}
