import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { extract } from '@app/core';
import { Shell } from '@app/shell/shell.service';
import { EntriesComponent } from './entries.component';

const routes: Routes = [
  Shell.childRoutes([
    { path: 'entries/:id', component: EntriesComponent, data: { title: extract('Entries') } }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EntriesRoutingModule {}
