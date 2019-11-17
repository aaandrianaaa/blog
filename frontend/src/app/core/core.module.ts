import {NgModule} from '@angular/core';
import {AuthService} from './services/auth.service';
import {ApiService} from './services/api.service';
import {UserService} from './services/user.service';
import {HTTP_INTERCEPTORS, HttpClient} from '@angular/common/http';
import {CommonModule} from '@angular/common';
import {HttpTokenInterceptor} from './interceptors/http.token.interceptors';
import {LocalStorageService} from './services/localStorage.service';

@NgModule({
  imports: [
    CommonModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpTokenInterceptor, multi: true},
    HttpClient,
    LocalStorageService,
    ApiService,
    AuthService,
    UserService,
  ]
})

export class CoreModule {}
