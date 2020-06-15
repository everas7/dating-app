import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {
  @Input() user: User;
  constructor(private userService: UserService, private toast: ToastrService) {}

  ngOnInit() {}

  like(id: number) {
    this.userService.likeUser(id).subscribe(() => {
      this.user.liked = true;
      this.toast.success(`You have liked ${this.user.knownAs}`);
    });
  }

  dislike(id: number) {
    this.userService.dislikeUser(id).subscribe(() => {
      this.user.liked = false;
      this.toast.info(`You have disliked ${this.user.knownAs}`);
    });
  }
}
