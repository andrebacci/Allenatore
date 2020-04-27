import { Component } from '@angular/core';
import { TransferService } from '../../services/transferService';
import { Transfer } from '../../models/transfer';
import { ResultData } from '../../models/resultData';
import { Team } from '../../models/team';
import { Player } from '../../models/player';
import { TeamService } from '../../services/team.service';
import { PlayerService } from '../../services/player.service';
import Utility from '../../utility/utility';
import { Router } from '@angular/router';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html'
})
export class TransferComponent {

  transfers: Transfer[] = [];
  teams: Team[] = [];
  players: Player[] = [];

  selectedTeamOld;
  selectedTeamNew;
  selectedPlayer;

  constructor(private transferService: TransferService, private teamService: TeamService, private playerService: PlayerService, private router: Router) {

  }

  ngOnInit(): void {
    this.getTeams();
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
    var transfer = new Transfer();
    transfer.idPlayer = this.selectedPlayer;
    transfer.idTeamNew = this.selectedTeamNew;
    transfer.idTeamOld = this.selectedTeamOld;

    this.transferService.insert(transfer).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        Utility.redirect('/transfer', this.router);
      } else {
        // Errore
      }
    })
  }

  // Evento quando si cambia la dropdown del team vecchio
  onChangeTeam(event: any): void {
    this.playerService.getByTeamId(this.selectedTeamOld).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.players = resultData.data as Player[];
      } else {
        // Errore
      }
    });
  }
}
