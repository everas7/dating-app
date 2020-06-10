import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() handleMainPhotoChange = new EventEmitter();
  baseUrl = environment.apiUrl;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  constructor(
    private authService: AuthService,
    private userService: UserService,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.intializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  setMain(photo: Photo) {
    this.userService
      .setMain(this.authService.decodedAccessToken.nameid, photo.id)
      .subscribe(() => {
        this.photos.filter(p => p.isMain)[0].isMain = false;
        photo.isMain = true;
        this.handleMainPhotoChange.emit(photo.url);
        this.authService.changeProfilePhotoUrl(photo.url);
      });
  }

  deletePhoto(photo: Photo) {
    this.userService
      .deletePhoto(this.authService.decodedAccessToken.nameid, photo.id)
      .subscribe(() => {
        this.photos.splice(
          this.photos.findIndex(p => p.id === photo.id),
          1
        );
        this.toast.success('Photo has been deleted');
      });
  }

  intializeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseUrl +
        `/users/${this.authService.decodedAccessToken.nameid}/photos`,
      authToken: `Bearer ${localStorage.getItem('jwt')}`,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = file => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status) => {
      const res: Photo = JSON.parse(response);
      const photo: Photo = {
        id: res.id,
        url: res.url,
        dateAdded: res.dateAdded,
        description: res.description,
        isMain: res.isMain
      };
      this.photos.push(photo);
      if (photo.isMain) {
        this.handleMainPhotoChange.emit(photo.url);
        this.authService.changeProfilePhotoUrl(photo.url);
      }
    };
  }
}
