import { Player } from "./player";

export class GameInfo {
  idGame: number = 0;

  idTeamHome: number = 0;
  idTeamAway: number = 0;
  
  formationHome: Player[];
  formationAway: Player[];

  scorerHome: Player[];
  scorerAway: Player[];
}
