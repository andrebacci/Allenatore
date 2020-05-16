export class GamePlayer {
  id: number = 0;

  round: number = 0;

  idTeamHome: number = 0;
  idTeamAway: number = 0;

  golTeamHome: number;
  golTeamAway: number;

  teamHome: string = "";
  teamAway: string = "";

  timeIn: number = -1;
  timeOut: number = -1;

  info: string = "";
}
