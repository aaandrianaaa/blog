import {ApiService} from './api.service';
import {Injectable} from '@angular/core';

@Injectable()
export class LocalStorageService {
  constructor() {}

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
   return  localStorage.getItem('token');
  }

  destroyToken() {
    localStorage.removeItem('token');
  }
}


