import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PeopleComponent } from './people/people.component';
import { MessagesComponent } from './messages/messages.component';
import { LandingComponent } from './landing/landing.component';
import { ProfileDetailComponent } from './profiles/profile-detail/profile-detail.component';
import { ProfileDetailsResolver } from './_resolvers/profile-detail.resolver';
import { PeopleListResolver } from './_resolvers/people-list.resolver';
import { MatchesResolver } from './_resolvers/matches.resolver';
import { ProfileEditComponent } from './profiles/profile-edit/profile-edit.component';
import { ProfileEditResolver } from './_resolvers/profile-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MatchesComponent } from './matches/matches.component';

export const appRoutes: Routes = [
  {
    path: '',
    component: LandingComponent,
    outlet: 'publicOutlet'
  },
  {
    path: '',
    children: [
      {
        path: 'people',
        component: PeopleComponent,
        resolve: { users: PeopleListResolver }
      },
      {
        path: 'matches',
        component: MatchesComponent,
        resolve: { users: MatchesResolver }
      },
      { path: 'messages', component: MessagesComponent },
      {
        path: 'profile/edit',
        component: ProfileEditComponent,
        resolve: { user: ProfileEditResolver },
        canDeactivate: [PreventUnsavedChanges]
      },
      {
        path: 'profile/:username',
        component: ProfileDetailComponent,
        resolve: { user: ProfileDetailsResolver }
      },
      {
        path: '',
        redirectTo: 'people',
        pathMatch: 'full'
      }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
