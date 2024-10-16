import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MovieRoutingModule } from './movie-routing.module';
import { MovieComponent } from './movie.component';
import { MyListComponent } from './my-list/my-list.component';
import { CardComponent } from './card/card.component';
import { DetailsComponent } from './details/details.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { UpdateMovieComponent } from './update-movie/update-movie.component';


@NgModule({
  declarations: [
    MovieComponent,
    MyListComponent,
    CardComponent,
    DetailsComponent,
    AddMovieComponent,
    UpdateMovieComponent
  ],
  imports: [
    SharedModule,
    MovieRoutingModule,
    ReactiveFormsModule,
  ]
})
export class MovieModule { }
