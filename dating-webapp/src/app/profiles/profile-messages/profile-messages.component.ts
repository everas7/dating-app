import {
  Component,
  OnInit,
  Input,
  ViewChild,
  ElementRef,
  AfterViewChecked
} from '@angular/core';
import { Message } from 'src/app/_models/message';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile-messages',
  templateUrl: './profile-messages.component.html',
  styleUrls: ['./profile-messages.component.css']
})
export class ProfileMessagesComponent implements OnInit, AfterViewChecked {
  @ViewChild('chat', { static: false }) chatContainer: ElementRef;
  @Input() recipientId: number;
  messages: Message[];
  newMessage: Partial<Message> = {};

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.loadMessages();
    this.scrollToBottom();
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
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

  scrollToBottom(): void {
    try {
      this.chatContainer.nativeElement.scrollTop = this.chatContainer.nativeElement.scrollHeight;
    } catch (err) {}
  }

  sendMessage() {
    this.newMessage.recipientId = this.recipientId;
    this.userService
      .sendMessage(
        this.authService.decodedAccessToken.nameid,
        this.newMessage as Message
      )
      .subscribe(
        message => {
          this.messages.push(message);
          this.newMessage.content = '';
        },
        err => {
          this.toast.error('Error sending message');
        }
      );
  }
}
