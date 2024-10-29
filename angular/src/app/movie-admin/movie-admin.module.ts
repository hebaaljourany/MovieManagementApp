import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { MovieAdminRoutingModule } from './movie-admin-routing.module';
import { MovieAdminComponent } from './movie-admin.component';
import { DetailsAdminComponent } from './details-admin/details-admin.component';


@NgModule({
  declarations: [
    MovieAdminComponent,
    DetailsAdminComponent
  ],
  imports: [
    SharedModule,
    MovieAdminRoutingModule
  ]
})
export class MovieAdminModule { }
