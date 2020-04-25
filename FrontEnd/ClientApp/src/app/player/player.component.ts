import { Component } from "@angular/core";
import { Player } from "../../models/player";
import { PlayerService } from "../../services/player.component";
import { ActivatedRoute, Router } from "@angular/router";
import { ResultData } from "../../models/resultData";

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
})

export class PlayerComponent {

  player: Player = null;

  isReadOnly: boolean = false;

  idPlayer: number = -1;

  sub: any;

  mode: string = "";

  lastname: string = "";
  firstname: string = "";
  age: number = 0;
  lastteam: string = "";
  details: string = ""
  feet: number = 0;
  role: number = 0;
  penalty: string = "";

  constructor(private playerService: PlayerService, private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.mode = params['mode'];

      if (this.mode == "create") {
        this.isReadOnly = false;
      } else {
        this.idPlayer = params['id'];

        if (this.mode == "update") {
          this.isReadOnly = false;
        } else if (this.mode == "detail") {
          this.isReadOnly = true;
        }

        // Recupero le informazioni del giocatore
        this.getPlayerById(this.idPlayer);
      }
    });
  }

  // Inizializzo il giocatore dato il suo id
  getPlayerById(id: number): void {
    this.playerService.getById(id).subscribe(res => {
      var resultData = res as ResultData;
      if (resultData.status) {
        this.player = resultData.data as Player;

        this.initForm();
      } else {
        // Errore
      }
    })
  }

  // Inizializzo i campi della form
  initForm(): void {
    this.lastname = this.player.lastname;
    this.firstname = this.player.firstname;
    this.age = this.player.age;
    this.feet = this.player.feet;
    this.role = this.player.role;
    this.lastteam = this.player.lastTeamString;
    this.penalty = this.player.penalty.toString();
    this.details = this.player.details;
  }

  // Redirect della pagina
  redirect(url: string): void {
    this.router.navigate([]).then(res => { window.open(url, '_self') });
  }

  // Passa dalla modalità "detail" alla modalità "update"
  update(): void {
    this.redirect('/player/update/' + this.player.id);
  }

  // Salva le modifiche
  save(): void {
    var a = this.feet;
    var b = this.role;
    var c = this.penalty;
  }
}
