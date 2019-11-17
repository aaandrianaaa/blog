import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ViewComponent} from './view/view.component';

const routes: Routes = [
  {
    path: '/profile/:id',
    component: ViewComponent,
  },
  {
    path: '/profile',
    component: ViewComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class UserRoutingModule {}
