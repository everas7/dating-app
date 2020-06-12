import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User, UserFilters } from '../_models/user';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResponseEnvelope } from '../_models/pagination';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {
  users: User[] = [];
  pagination: Pagination;
  filters: any = {};
  genderList = [
    { value: 'female', display: 'Females' },
    { value: 'male', display: 'Males' }
  ];
  constructor(
    private userService: UserService,
    private authService: AuthService,
    private router: ActivatedRoute
  ) {}

  ngOnInit() {
    this.router.data.subscribe(data => {
      this.users = data.users.response;
      this.pagination = data.users.pagination;
    });
    this.setDefaultFilters();
  }

  pageChanged(event) {
    this.pagination.page = event.page;
    this.loadUsers();
  }

  setDefaultFilters() {
    this.filters.gender =
      this.authService.currentUser.gender === 'male' ? 'female' : 'male';
    this.filters.minAge = 18;
    this.filters.maxAge = 99;
  }

  resetFilters() {
    this.setDefaultFilters();
    this.loadUsers();
  }

  loadUsers() {
    this.userService
      .getUsers(this.pagination.page, this.pagination.perPage, this.filters)
      .subscribe((pagRespo: PaginatedResponseEnvelope<User[]>) => {
        this.users = pagRespo.response;
        console.log('anja', pagRespo.pagination);
        this.pagination = pagRespo.pagination;
      });
  }
}
