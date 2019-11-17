import {ApiService} from './api.service';
import {Injectable} from '@angular/core';

@Injectable()
export class UserService {
  constructor(
    private http: ApiService,
  ) {}

  register(credentials: any) {
    return this.http.post('/users', credentials );
  }
}
