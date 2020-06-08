import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MatchesComponent } from './matches/matches.component';
import { MessagesComponent } from './messages/messages.component';
import { LandingComponent } from './landing/landing.component';

export const appRoutes: Routes = [
  {
    path: '',
    component: LandingComponent,
    outlet: 'publicOutlet'
  },
  {
    path: '',
    children: [
      { path: '', component: HomeComponent, },
      { path: 'matches', component: MatchesComponent },
      { path: 'messages', component: MessagesComponent }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
