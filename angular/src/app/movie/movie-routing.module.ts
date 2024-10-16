import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieComponent } from './movie.component';
import { MyListComponent } from './my-list/my-list.component';
import { DetailsComponent } from './details/details.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { UpdateMovieComponent } from './update-movie/update-movie.component';
import { CardComponent } from './card/card.component';
import { AuthGuard, PermissionGuard } from '@abp/ng.core';


const routes: Routes = [{
  path: '',
   component: MovieComponent ,
   children: [
    /*{ path: 'my-list', component: MyListComponent },
    { path: 'details/:id', component: DetailsComponent },
    { path: 'add-movie', component: AddMovieComponent },
    { path: 'update-movie/:id', component: UpdateMovieComponent },*/
    { path: 'card/:id', component: CardComponent }

  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MovieRoutingModule { }
