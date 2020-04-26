import { Component } from "@angular/core";

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
})

export class GameComponent {

  game: any = null;

  isReadOnly: boolean = false;

  constructor() {
    this.isReadOnly = true;
  }
}
