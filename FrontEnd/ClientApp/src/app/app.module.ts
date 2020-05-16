import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

// Component
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CalendarComponent } from './calendar/calendar.component';
import { GameComponent } from './game/game.component';
import { GameInfoComponent } from './game-info/game-info.component';
import { LoginComponent } from './login/login.component';
import { PlayerComponent } from './player/player.component';
import { PlayersComponent } from './players/players.component';
import { RankingComponent } from './ranking/ranking.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { TeamComponent } from './team/team.component';
import { TeamsComponent } from './teams/teams.component';
import { TransferComponent } from './transfer/transfer.component';
import { TransfersComponent } from './transfers/transfers.component';
import { UtilityComponent } from './utility/utility.component';

// Servizi
import { FeetService } from '../services/feet.service';
import { GameService } from '../services/game.service';
import { PlayerService } from '../services/player.service';
import { RankingService } from '../services/ranking.service';
import { RoleService } from '../services/role.service';
import { RoundService } from '../services/round.service';
import { TeamService } from '../services/team.service';
import { TransferService } from '../services/transferService';
import { UtilityService } from '../services/utility.service';
import { LoginService } from '../services/login.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CalendarComponent,
    GameComponent,
    GameInfoComponent,
    LoginComponent,
    PlayerComponent,
    PlayersComponent,
    RankingComponent,
    SidebarComponent,
    TeamComponent,
    TeamsComponent,
    TransferComponent,
    TransfersComponent,
    UtilityComponent     
  ],
  imports: [
    AngularFontAwesomeModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'calendar', component: CalendarComponent },
      { path: 'game/:mode', component: GameComponent },
      { path: 'game/:mode/:id', component: GameComponent },
      { path: 'game-info/:mode/:id', component: GameInfoComponent },
      { path: 'login', component: LoginComponent },
      { path: 'player/:mode', component: PlayerComponent },
      { path: 'player/:mode/:id', component: PlayerComponent },
      { path: 'players', component: PlayersComponent },
      { path: 'ranking/:mode', component: RankingComponent },
      { path: 'team/:mode', component: TeamComponent },
      { path: 'team/:mode/:id', component: TeamComponent },
      { path: 'teams', component: TeamsComponent },
      { path: 'transfer/create', component: TransferComponent },
      { path: 'transfers', component: TransfersComponent },
      { path: 'utility', component: UtilityComponent }
    ])
  ],
  providers: [
    FeetService,
    GameService,
    PlayerService,
    RankingService,
    RoleService,
    RoundService,
    TeamService,
    TransferService,
    UtilityService,
    LoginService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
