import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { extract } from '@app/core';
import { Shell } from '@app/shell/shell.service';
import { PhoneBooksComponent } from './phone-books.component';

const routes: Routes = [
  Shell.childRoutes([{ path: 'phoneBooks', component: PhoneBooksComponent, data: { title: extract('Phone Books') } }])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PhoneBooksRoutingModule {}
