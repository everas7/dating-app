import { Injectable } from '@angular/core';
import {
  Resolve,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Message } from '../_models/message';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class MessagesResolver implements Resolve<Message[]> {
  page = 1;
  perPage = 5;
  messageContainer = 'Unread';
  constructor(
    private userService: UserService,
    private authService: AuthService,
    private router: Router,
    private toast: ToastrService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Message[]> {
    return this.userService
      .getMessages(this.authService.currentUser.id, this.page, this.perPage, this.messageContainer)
      .pipe(
        catchError(error => {
          this.toast.error('Problem getting messages');
          this.router.navigate(['/']);
          return of(null);
        })
      );
  }
}
