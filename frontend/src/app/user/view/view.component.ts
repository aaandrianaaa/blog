import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {AuthService} from '../../core/services/auth.service';
import {LocalStorageService} from '../../core/services/localStorage.service';
import {TokenModel} from '../../core/models/token.model';
import {catchError} from 'rxjs/operators';
import {UserService} from '../../core/services/user.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-user-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
  ngOnInit(): void {
  }
}
