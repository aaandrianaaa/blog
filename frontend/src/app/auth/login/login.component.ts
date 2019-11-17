import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../../core/services/auth.service';
import {catchError} from 'rxjs/operators';
import {LocalStorageService} from '../../core/services/localStorage.service';
import {TokenModel} from '../../core/models/token.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  implements OnInit {
  loginForm: FormGroup;

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private lStorage: LocalStorageService,
  ) {
    this.loginForm = fb.group({
      email: '',
      password: ''
    });
  }

  ngOnInit(): void {
  }

  submit() {
    this.authService.login(this.loginForm.value).subscribe(
      (response: TokenModel) => {
        this.lStorage.saveToken(response.token);
      },
      error => catchError(error),
    );
  }

}
