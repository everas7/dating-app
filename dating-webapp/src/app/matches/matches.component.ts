import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResponseEnvelope } from '../_models/pagination';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {
  users: User[] = [];
  pagination: Pagination;
  constructor(
    private userService: UserService,
    private router: ActivatedRoute
  ) {}

  ngOnInit() {
    this.router.data.subscribe(data => {
      this.users = data.users.response;
      this.pagination = data.users.pagination;
    });
  }

  pageChanged(event) {
    this.pagination.page = event.page;
    this.loadUsers();
  }

  loadUsers() {
    this.userService
      .getUsers(this.pagination.page, this.pagination.perPage)
      .subscribe((pagRespo: PaginatedResponseEnvelope<User[]>) => {
        this.users = pagRespo.response;
        this.pagination = pagRespo.pagination;
      });
  }
}
