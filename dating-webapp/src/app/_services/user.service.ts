import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User, UserFilters } from '../_models/user';
import { PaginatedResponseEnvelope } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + '/users';
  constructor(private http: HttpClient) {}

  getUsers(
    page?,
    perPage?,
    filters?: UserFilters,
    likesParam?: string,
  ): Observable<PaginatedResponseEnvelope<User[]>> {
    const paginatedResult: PaginatedResponseEnvelope<User[]> = new PaginatedResponseEnvelope<
      User[]
    >();
    let params = new HttpParams()
      .append('page', page)
      .append('perPage', perPage);
    if (filters) {
      params = params.append('gender', filters.gender);
      params = params.append('minAge', String(filters.minAge));
      params = params.append('maxAge', String(filters.maxAge));
      params = params.append('sortBy', filters.sortBy);
      params = params.append('sortOrder', filters.sortOrder);
    }

    if (likesParam) {
      params = params.append(likesParam, 'true');
    }

    return this.http
      .get<User[]>(this.baseUrl.toString(), {
        observe: 'response',
        params
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

  getMatches(
    page?,
    perPage?,
    filters?: UserFilters
  ): Observable<PaginatedResponseEnvelope<User[]>> {
    const paginatedResult: PaginatedResponseEnvelope<User[]> = new PaginatedResponseEnvelope<
      User[]
    >();
    let params = new HttpParams()
      .append('page', page)
      .append('perPage', perPage);
    if (filters) {
      params = params.append('gender', filters.gender);
      params = params.append('minAge', String(filters.minAge));
      params = params.append('maxAge', String(filters.maxAge));
      params = params.append('sortBy', filters.sortBy);
      params = params.append('sortOrder', filters.sortOrder);
    }
    return this.http
      .get<User[]>(this.baseUrl + '/self/matches', {
        observe: 'response',
        params
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

  likeUser(id: number): Observable<void> {
    return this.http.post<void>(this.baseUrl + `/${id}/like`, {});
  }

  dislikeUser(id: number): Observable<void> {
    return this.http.delete<void>(this.baseUrl + `/${id}/like`, {});
  }
}
