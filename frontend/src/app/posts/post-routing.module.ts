import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {AddComponent} from './add/add.component';

const routes: Routes = [
  {
    path: '/add',
    component: AddComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class PostRoutingModule {}
