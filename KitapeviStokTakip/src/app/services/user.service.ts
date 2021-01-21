import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { url } from '../shared/url';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

    getUsers() {
        return this.http.get<User[]>(`${url}users`);
    }

    getUserById(id: number) {
        return this.http.get<User>(`${url}users/${id}`);
    }

    deleteUser(id): Observable<User> {
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
        }),
      };
      return this.http.delete<User>(url+`users/${id}`);
    }
}
