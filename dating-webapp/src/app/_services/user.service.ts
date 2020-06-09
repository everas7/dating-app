import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + '/users';
  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl.toString());
  }

  getUser(usernameOrId: string): Observable<User> {
    return this.http.get<User>(this.baseUrl.toString() + `/${usernameOrId}`);
  }

  getSelf(): Observable<User> {
    return this.http.get<User>(this.baseUrl + '/self');
  }

  updateUser(id: number, user: User): Observable<void> {
    return this.http.put<void>(this.baseUrl.toString() + `/${id}`, user);
  }

  setMain(userId: number, photoId: number): Observable<void> {
    return this.http.post<void>(
      this.baseUrl + `/${userId}/photos/${photoId}/setmain`,
      {}
    );
  }
}
