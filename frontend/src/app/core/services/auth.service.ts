import {ApiService} from './api.service';
import {Injectable} from '@angular/core';

@Injectable()
export class AuthService {
  constructor(
    private http: ApiService,
  ) {}

  login(credentials: any) {
    return this.http.post('/auth/login', credentials);
  }

  // logout(credentials: any) {
  //   return this.http.post('/auth/logout);
  // }
}
