import { Injectable } from '@angular/core';
import { ProfileEditComponent } from '../profiles/profile-edit/profile-edit.component';
import { CanDeactivate } from '@angular/router';

@Injectable()
export class PreventUnsavedChanges
  implements CanDeactivate<ProfileEditComponent> {
  canDeactivate(component: ProfileEditComponent) {
    if (component && component.editProfileForm.dirty) {
      return confirm(
        'Are you sure you want to leave this page? Unsaved changes will be lost'
      );
    }
    return true;
  }
}
