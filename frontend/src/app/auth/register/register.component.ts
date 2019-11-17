import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {AuthService} from '../../core/services/auth.service';
import {LocalStorageService} from '../../core/services/localStorage.service';
import {TokenModel} from '../../core/models/token.model';
import {catchError} from 'rxjs/operators';
import {UserService} from '../../core/services/user.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  registerForm: FormGroup;
  private newPatientForm: any;

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.registerForm = fb.group({
      first_name: '',
      last_name: '',
      nickname: '',
      email: '',
      password: '',
      confirm_password: '',
      about_user: '',
      birthday_date: ''

    });
  }
  ngOnInit(): void {
  }

  submit() {
    const date = this.registerForm.value.birthday_date;
    this.registerForm.value.birthday_date = new Date(this.registerForm.value.birthday_date).valueOf() / 1000;
    this.userService.register(this.registerForm.value).subscribe(
     response => {
       this.router.navigateByUrl('/auth/login');
       this.registerForm.value.birthday_date = date;
     },
     error => {
       this.registerForm.value.birthday_date = date;
       catchError(error);
     }
   );
  }
}
