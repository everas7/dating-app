import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginatedResponseEnvelope } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + '/users';
  constructor(private http: HttpClient) {}

  getUsers(page?, perPage?): Observable<PaginatedResponseEnvelope<User[]>> {
    const paginatedResult: PaginatedResponseEnvelope<User[]> = new PaginatedResponseEnvelope<
      User[]
    >();
    return this.http
      .get<User[]>(this.baseUrl.toString(), {
        observe: 'response',
        params: {
          page,
          perPage
        }
      })
      .pipe(
        map(response => {
          console.log(response.headers);
          paginatedResult.pagination = {
            page: +response.headers.get('Page'),
            perPage: +response.headers.get('PerPage'),
            totalItems: +response.headers.get('TotalItems'),
            totalPages: +response.headers.get('TotalPages')
          };

          paginatedResult.response = response.body;
          return paginatedResult;
        })
      );
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

  deletePhoto(userId: number, photoId: number): Observable<void> {
    return this.http.delete<void>(
      this.baseUrl + `/${userId}/photos/${photoId}`
    );
  }
}
