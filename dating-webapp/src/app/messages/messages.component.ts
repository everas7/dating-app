import { Component, OnInit } from '@angular/core';
import { Pagination, PaginatedResponseEnvelope } from '../_models/pagination';
import { Message } from '../_models/message';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  pagination: Pagination;
  messageContainer = 'Unread';

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.messages = data.messages.response;
      this.pagination = data.messages.pagination;
    });
  }

  loadMessages() {
    this.userService
      .getMessages(
        this.authService.currentUser.id,
        this.pagination.page,
        this.pagination.perPage,
        this.messageContainer
      )
      .subscribe(
        (res: PaginatedResponseEnvelope<Message[]>) => {
          this.messages = res.response;
          this.pagination = res.pagination;
        },
        err => {
          this.toast.error(err);
        }
      );
  }

  pageChanged(event: any): void {
    this.pagination.page = event.page;
    this.loadMessages();
  }

  deleteMessage(id: number) {
    this.userService
      .deleteMessage(id, this.authService.decodedAccessToken.nameid)
      .subscribe(() => {
        this.messages.splice(
          this.messages.findIndex(m => m.id === id),
          1
        );
        this.toast.success('Message has been deleted');
      });
  }
}
