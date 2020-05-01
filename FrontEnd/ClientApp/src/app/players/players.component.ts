import { Component } from '@angular/core';
import { TeamService } from '../../services/team.service';
import { Team } from '../../models/team';
import { ResultData } from '../../models/resultData';
import { Router } from '@angular/router';
import Utility from '../../utility/utility';
import { PlayerService } from '../../services/player.service';
import { Player } from '../../models/player';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
})

export class PlayersComponent {

  players: Player[] = [];

  constructor(private playerService: PlayerService, private router: Router) {

  }

  ngOnInit(): void {
    this.getAll();
  }

  // Inizializza la lista con tutti i giocatori
  getAll(): void {
    this.playerService.getAll().subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.players = resultData.data as Player[];
      } else {
        // Errore
      }
    })
  }

  // Apre la pagina di dettaglio di un giocatore
  detailPlayer(id: number): void {
    Utility.redirect('/player/detail/' + id, this.router);
  }
}
