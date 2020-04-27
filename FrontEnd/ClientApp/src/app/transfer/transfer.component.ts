import { Component } from '@angular/core';
import { TransferService } from '../../services/transferService';
import { Transfer } from '../../models/transfer';
import { ResultData } from '../../models/resultData';
import { Team } from '../../models/team';
import { Player } from '../../models/player';
import { TeamService } from '../../services/team.service';
import { PlayerService } from '../../services/player.service';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html'
})
export class TransferComponent {

  transfers: Transfer[] = [];

  teams: Team[] = [];

  players: Player[] = [];

  constructor(private transferService: TransferService, private teamService: TeamService, private playerService: PlayerService) {

  }

  ngOnInit(): void {
    
  }

  // Inizializza la lista delle squadre
  getTeams(): void {
    this.teamService.getAll().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.teams = resultData.data as Team[];
      } else {
        // Errore
      }
    });
  }

  // Inizializza la lista dei giocatori dato l'id di una squadra
  getPlayersByIdTeam(id: number): void {
    this.playerService.getByTeamId(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.players = resultData.data as Player[];
      } else {
        // Errore
      }
    })
  }

  save(): void {

  }
}
