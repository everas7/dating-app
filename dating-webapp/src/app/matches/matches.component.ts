import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { Pagination, PaginatedResponseEnvelope } from '../_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {
  users: User[] = [];
  pagination: Pagination;
  likesParam: string;
  constructor(
    private userService: UserService,
    private router: ActivatedRoute
  ) {}

  ngOnInit() {
    this.router.data.subscribe(data => {
      this.users = data.users.response;
      this.pagination = data.users.pagination;
    });
    this.likesParam = 'Matches';
  }

  pageChanged(event) {
    this.pagination.page = event.page;
    this.loadUsers();
  }

  getHeaderText() {
    switch (this.likesParam) {
      case 'Matches':
        return this.pagination.totalItems
          ? `Your matches (${this.pagination.totalItems})`
          : 'You have no matches yet';
      case 'Likers':
        return this.pagination.totalItems
          ? `People that liked you (${this.pagination.totalItems})`
          : 'You have no likes yet';
      default:
        return this.pagination.totalItems
          ? `People that you liked (${this.pagination.totalItems})`
          : 'You have not liked anyone yet';
    }
  }

  loadUsers() {
    this.userService
      .getUsers(
        this.pagination.page,
        this.pagination.perPage,
        null,
        this.likesParam
      )
      .subscribe((pagRespo: PaginatedResponseEnvelope<User[]>) => {
        this.users = pagRespo.response;
        this.pagination = pagRespo.pagination;
      });
  }
}
