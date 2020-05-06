import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { TeamService } from '../../services/team.service';
import { Team } from '../../models/team';
import { ResultData } from '../../models/resultData';
import { ActivatedRoute, Router } from '@angular/router';
import { Player } from '../../models/player';
import { Game } from '../../models/game';
import { PlayerService } from '../../services/player.service';

import Utility from '../../utility/utility';
import { GameService } from '../../services/game.service';
import { Transfer } from '../../models/transfer';
import { TransferService } from '../../services/transferService';

import * as Chart from 'chart.js';
import { TeamStatistics } from '../../models/teamStatistics';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
})

export class TeamComponent implements AfterViewInit {

  team: Team = null;

  players: Player[] = [];

  games: Game[] = [];

  transfers: Transfer[] = [];
  transfersIn: Transfer[] = [];
  transfersOut: Transfer[] = [];

  statistics: TeamStatistics = null;

  idTeam: number = -1;

  sub: any;

  mode: string = "";

  teamName: string = "";
  teamCity: string = "";
  teamCategory: string = "";
  teamMister: string = "";

  isReadOnly: boolean = false;

  errorModalIsOpen: boolean = false;

  messageError: string = "";
  messageNoPlayer: string = "";
  messageNoGames: string = "";
  messageNoTransfers: string = "";

  canvasGames: any;
  canvasGamesHome: any;
  canvasGamesAway: any;

  canvasGols: any;
  canvasGolsHome: any;
  canvasGolsAway: any;

  ctx: any;

  constructor(private teamService: TeamService, private playerService: PlayerService, private gameService: GameService, private transferService: TransferService,
    private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {      
      this.mode = params['mode'];

      if (this.mode == "create") {
        this.isReadOnly = false;
      } else {
        this.idTeam = params['id'];

        if (this.mode == "update") {
          this.isReadOnly = false;          
        } else if (this.mode == "detail") {
          this.isReadOnly = true;          
        }

        // Recupero le informazioni della squadra
        this.getTeamById(this.idTeam);

        // Recupero le partite della squadra
        this.getGames(this.idTeam);

        // Recupero i trasferimenti
        this.getTransfers(this.idTeam);

        // Recupero le statistiche
        this.getStatistics(this.idTeam);
      }
    });
  }

  ngAfterViewInit() {
    
  }

  // Inizializza il chart delle partite 
  initChartGames(): void {
    this.canvasGames = document.getElementById("chart-games");
    this.ctx = this.canvasGames.getContext("2d");

    let chart = new Chart(this.ctx, {
      type: 'pie',
      data: {
        labels: ["Vinte", "Pareggiate", "Perse"],
        datasets: [{
          //label: '# of Votes',
          data: [this.statistics.wins, this.statistics.draws, this.statistics.losts],
          backgroundColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)'
          ],
          borderWidth: 1
        }]
      }
    });
  }

  // Inizializza il chart delle partite in casa
  initChartGamesHome(): void {
    this.canvasGamesHome = document.getElementById("chart-games-home");
    this.ctx = this.canvasGamesHome.getContext("2d");

    let chart = new Chart(this.ctx, {
      type: 'pie',
      data: {
        labels: ["Vinte", "Pareggiate", "Perse"],
        datasets: [{
          data: [this.statistics.wins, this.statistics.draws, this.statistics.losts],
          backgroundColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)'
          ],
          borderWidth: 1
        }]
      }
    });
  }

  // Inizializza il chart delle partite in trasferta
  initChartGamesAway(): void {
    this.canvasGamesAway = document.getElementById("chart-games-away");
    this.ctx = this.canvasGamesAway.getContext("2d");

    let chart = new Chart(this.ctx, {
      type: 'pie',
      data: {
        labels: ["Vinte", "Pareggiate", "Perse"],
        datasets: [{
          data: [this.statistics.wins, this.statistics.draws, this.statistics.losts],
          backgroundColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)'
          ],
          borderWidth: 1
        }]
      }
    });
  }

  // Inizializza il chart dei gol 
  initChartGols(): void {
    this.canvasGols = document.getElementById("chart-gols");
    this.ctx = this.canvasGames.getContext("2d");

    let chart = new Chart(this.ctx, {
      type: 'pie',
      data: {
        labels: ["Gol Fatti", "Gol Subiti"],
        datasets: [{
          //label: '# of Votes',
          data: [this.statistics.scoredGols, this.statistics.concededGols],
          backgroundColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
          ],
          borderWidth: 1
        }]
      }
    });
  }

  // Inizializza il chart dei gol in casa
  initChartGolsHome(): void {
    this.canvasGolsHome = document.getElementById("chart-gols-home");
      this.ctx = this.canvasGolsHome.getContext("2d");

    let chart = new Chart(this.ctx, {
      type: 'pie',
      data: {
        labels: ["Gol Fatti", "Gol Subiti"],
        datasets: [{
          //label: '# of Votes',
          data: [this.statistics.scoredGolsHome, this.statistics.concededGolsAway],
          backgroundColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
          ],
          borderWidth: 1
        }]
      }
    });
  }

  // Inizializza il chart dei gol in trasferta
  initChartGolsAway(): void {
    this.canvasGolsAway = document.getElementById("chart-gols-away");
    this.ctx = this.canvasGames.getContext("2d");

    let chart = new Chart(this.ctx, {
      type: 'pie',
      data: {
        labels: ["Gol Fatti", "Gol Subiti"],
        datasets: [{
          //label: '# of Votes',
          data: [this.statistics.scoredGolsAway, this.statistics.concededGolsAway],
          backgroundColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
          ],
          borderWidth: 1
        }]
      }
    });
  }

  // Recupero il team dato il suo id
  getTeamById(id: number): void {
    this.teamService.getById(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.team = resultData.data as Team;

        // Inizializzo la form una volta che ho il team
        this.initForm();

        // Recupero la rosa della squadra
        this.getPlayers();
      } else {
        // Errore
      }
    });
  }

  // Recupero le partite
  getGames(id: number): void {
    this.gameService.getByIdTeam(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.games = resultData.data as Game[];

        if (this.games.length == 0) {
          this.messageNoGames = "Non ci sono partite";
        }
      } else {
        // Errore
      }
    })
  }

  // Recupero i trasferimenti della squadra
  getTransfers(id: number): void {
    this.transferService.getByIdTeam(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.transfers = resultData.data as Transfer[];

        if (this.transfers.length == 0) {
          this.messageNoTransfers = "Non ci sono trasferimenti.";
        } else {
          // Popolare i due array (transfersIn e transfersOut) --> SI PUO' FARE MEGLIO?
          for (var i = 0; i < this.transfers.length; i++) {
            if (this.transfers[i].idTeamNew == id) {
              this.transfersIn.push(this.transfers[i]);
            } else {
              this.transfersOut.push(this.transfers[i]);
            }
          }
        }
      } else {
        // Errore
      }
    })
  }

  // Recupera le statistiche
  getStatistics(id: number): void {
    this.teamService.getStatistics(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.statistics = resultData.data as TeamStatistics;

        // Inizializzo i chart
        this.initCharts();
      } else {
        // Errore
      }
    });
  }

  // Inizializza tutti i charts
    initCharts(): void {

    this.initChartGames();
    this.initChartGamesHome();
    this.initChartGamesAway();
    this.initChartGols();
    this.initChartGolsHome();
    this.initChartGolsAway();
      
  }

  // Inizializza i campi della form
  initForm(): void {
    this.teamName = this.team.name;
    this.teamCity = this.team.city;
    this.teamCategory = this.team.category;
    this.teamMister = this.team.mister;    
  }

  // Svuota tutti i campi
  cleanForm(): void {
    this.teamName = "";
    this.teamCity = "";
    this.teamCategory = "";
    this.teamMister = "";
  }

  // Salva le modifiche
  save(createAfter: boolean): void {
    if (this.team == null)
      this.team = new Team(this.teamName);

    this.team.city = this.teamCity;
    this.team.category = this.teamCategory;
    this.team.mister = this.teamMister;

    if (this.mode == "update") {
      this.teamService.update(this.team).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {
          let team = resultData.data as Team;
          Utility.redirect('/team/detail/' + team.id, this.router);
        } else {
          // Errore
        }
      })
    } else if (this.mode == "create") {
      this.teamService.insert(this.team).subscribe(res => {
        var resultData = res as ResultData;
        if (resultData.status) {
          let team = resultData.data as Team;

          if (createAfter) {
            this.cleanForm();
          } else {
            Utility.redirect('/team/detail/' + team.id, this.router);
          }
        } else {
          this.errorModalIsOpen = true;
          this.messageError = resultData.message;
        }
      })
    } else {
      // Errore
    }
  }

  // Annulla le modifiche fatte
  undo(): void {
    if (this.mode == "update") {
      this.initForm();
    } else if (this.mode == "create") {
      this.cleanForm();
    }
  }

  // Passa dalla modalità "detail" alla modalità "update"
  update(): void {
    Utility.redirect('/team/update/' + this.team.id, this.router);
  }

  // Recupero la rosa della squadra
  getPlayers(): void {
    this.playerService.getByTeamId(this.team.id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.players = resultData.data as Player[];

        if (this.players == null || this.players.length == 0) {
          this.messageNoPlayer = "Nessun giocatore per questa squadra.";
        }
      } else {
        // Errore
      }
    });
  }

  // Apre la pagina di dettaglio del giocatore
  detailPlayer(id: number): void {
    Utility.redirect('/player/detail/' + id, this.router);
  }

  // Apre la pagina di dettaglio della partita
  detailGame(id: number): void {
    Utility.redirect('/game/detail/' + id, this.router);
  }

  // Chiude la modale
  closeModal(): void {
    this.errorModalIsOpen = false;
  }
}
