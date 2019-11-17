import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {PostRoutingModule} from './post-routing.module';
import {ReactiveFormsModule} from '@angular/forms';
import {AddComponent} from './add/add.component';

@NgModule({
  declarations: [
    AddComponent
  ],
  imports: [
    PostRoutingModule,
    ReactiveFormsModule,
  ],
})
export class PostModule { }
