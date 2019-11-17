import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {UserRoutingModule} from './user-routing.module';
import {ReactiveFormsModule} from '@angular/forms';
import {ViewComponent} from './view/view.component';

@NgModule({
  declarations: [
    ViewComponent
  ],
  imports: [
    UserRoutingModule,
    ReactiveFormsModule,
  ],
})
export class UserModule { }
