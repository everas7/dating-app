import { Component, OnInit, Input } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile-messages',
  templateUrl: './profile-messages.component.html',
  styleUrls: ['./profile-messages.component.css']
})
export class ProfileMessagesComponent implements OnInit {
  @Input() recipientId: number;
  messages: Message[];

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages() {
    this.userService
      .getMessageThread(this.authService.currentUser.id, this.recipientId)
      .subscribe(
        messages => {
          this.messages = messages;
        },
        err => {
          this.toast.error(err);
        }
      );
  }
}
