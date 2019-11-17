import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {LocalStorageService} from '../services/localStorage.service';

@Injectable()
export class HttpTokenInterceptor implements HttpInterceptor {
  constructor(
   private lStorage: LocalStorageService
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.lStorage.getToken();

    const headersConfig = {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: ''
    };

    if (token) {
      headersConfig.Authorization = `Bearer ${token}`;
    }

    const request = req.clone({ setHeaders: headersConfig });
    return next.handle(request);
  }
}
