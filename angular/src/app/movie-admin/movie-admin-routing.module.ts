import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieAdminComponent } from './movie-admin.component';

const routes: Routes = [{ path: '', component: MovieAdminComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MovieAdminRoutingModule { }
