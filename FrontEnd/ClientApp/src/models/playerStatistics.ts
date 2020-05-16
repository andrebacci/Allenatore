export class PlayerStatistics {
  gamesAll: number = 0;
  minutes: number = 0;
  gols: number = 0;

  gamesHolder: number = 0;
  gamesIn: number = 0; // Subentrato
  gamesOut: number = 0; // Sotituito

  lastGame: Date = null;
  lastGameHolder: Date = null;
}
