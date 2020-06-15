import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PeopleComponent } from './people/people.component';
import { MessagesComponent } from './messages/messages.component';
import { LandingComponent } from './landing/landing.component';
import { ProfileDetailComponent } from './profiles/profile-detail/profile-detail.component';
import { ProfileDetailsResolver } from './_resolvers/profile-detail.resolver';
import { PeopleListResolver } from './_resolvers/people-list.resolver';
import { ProfileEditComponent } from './profiles/profile-edit/profile-edit.component';
import { ProfileEditResolver } from './_resolvers/profile-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';

export const appRoutes: Routes = [
  {
    path: '',
    component: LandingComponent,
    outlet: 'publicOutlet'
  },
  {
    path: '',
    children: [
      { path: '', component: HomeComponent },
      {
        path: 'people',
        component: PeopleComponent,
        resolve: { users: PeopleListResolver }
      },
      { path: 'messages', component: MessagesComponent },
      {
        path: 'profile/edit',
        component: ProfileEditComponent,
        resolve: { user: ProfileEditResolver },
        canDeactivate:[PreventUnsavedChanges]
      },
      {
        path: 'profile/:username',
        component: ProfileDetailComponent,
        resolve: { user: ProfileDetailsResolver }
      }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
