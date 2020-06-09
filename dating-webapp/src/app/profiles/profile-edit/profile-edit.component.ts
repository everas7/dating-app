import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit {
  @ViewChild('editProfileForm') editProfileForm: NgForm;
  user: User;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editProfileForm.dirty) {
      return ($event.returnValue = true);
    }
  }

  constructor(
    private router: ActivatedRoute,
    private userService: UserService,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.router.data.subscribe(data => {
      this.user = data.user;
    });
  }

  updateProfile() {
    this.userService.updateUser(this.user.id, this.user).subscribe(
      () => {
        this.editProfileForm.reset(this.user);
        this.toast.success('Profile info has been updated');
      },
      err => {
        this.toast.error('Error saving changes');
      }
    );
  }

  setMainPhoto(url: string) {
    this.user.photoUrl = url;
  }
}
