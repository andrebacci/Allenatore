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
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { GameComponent } from './game/game.component';
import { PlayerComponent } from './player/player.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { TeamComponent } from './team/team.component';
import { TeamsComponent } from './teams/teams.component';

// Servizi
import { FeetService } from '../services/feet.service';
import { GameService } from '../services/game.service';
import { PlayerService } from '../services/player.service';
import { RankingService } from '../services/ranking.service';
import { RoleService } from '../services/role.service';
import { TeamService } from '../services/team.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    GameComponent,
    PlayerComponent,
    SidebarComponent,
    TeamComponent,
    TeamsComponent
  ],
  imports: [
    AngularFontAwesomeModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'game/:mode', component: GameComponent },
      { path: 'game/:mode/:id', component: GameComponent },
      { path: 'player/:mode/:id', component: PlayerComponent },
      { path: 'team/:mode', component: TeamComponent },
      { path: 'team/:mode/:id', component: TeamComponent },
      { path: 'teams', component: TeamsComponent }
    ])
  ],
  providers: [
    FeetService,
    GameService,
    PlayerService,
    RankingService,
    RoleService,
    TeamService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
