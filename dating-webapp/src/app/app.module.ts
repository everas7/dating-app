import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FileUploadModule } from 'ng2-file-upload';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import {TimeagoModule} from 'ngx-timeago';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MatchesComponent } from './matches/matches.component';
import { MessagesComponent } from './messages/messages.component';
import { LandingComponent } from './landing/landing.component';
import { appRoutes } from './routes';
import { UserCardComponent } from './_common/users/user-card/user-card.component';
import { ProfileDetailComponent } from './profiles/profile-detail/profile-detail.component';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { UserService } from './_services/user.service';
import { ProfileDetailsResolver } from './_resolvers/profile-detail.resolver';
import { PeopleListResolver } from './_resolvers/people-list.resolver';
import { ProfileEditComponent } from './profiles/profile-edit/profile-edit.component';
import { ProfileEditResolver } from './_resolvers/profile-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { PhotoEditorComponent } from './profiles/photo-editor/photo-editor.component';

const tokenGetter = () => {
  return localStorage.getItem('jwt');
};

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    MatchesComponent,
    MessagesComponent,
    LandingComponent,
    UserCardComponent,
    ProfileDetailComponent,
    ProfileEditComponent,
    PhotoEditorComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right'
    }),
    PaginationModule.forRoot(),
    NgxGalleryModule,
    FileUploadModule,
    RouterModule.forRoot(appRoutes),
    TimeagoModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    })
  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    UserService,
    ProfileDetailsResolver,
    PeopleListResolver,
    ProfileEditResolver,
    PreventUnsavedChanges
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
