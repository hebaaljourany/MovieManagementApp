import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MovieRoutingModule } from './movie-routing.module';
import { MovieComponent } from './movie.component';
import { MyListComponent } from './my-list/my-list.component';
import { CardComponent } from './card/card.component';
import { DetailsComponent } from './details/details.component';
import { MovieFormComponent } from '../movie-admin/movie-form/movie-form.component';
import { ActorFormComponent } from '../movie-admin/actor-form/actor-form.component';
import { CategoryFormComponent } from '../movie-admin/category-form/category-form.component';


@NgModule({
  declarations: [
    MovieComponent,
    MyListComponent,
    CardComponent,
    DetailsComponent,
    MovieFormComponent,
    ActorFormComponent,
    CategoryFormComponent,
  ],
  imports: [
    SharedModule,
    MovieRoutingModule,
    ReactiveFormsModule,

  ]
})
export class MovieModule { }
