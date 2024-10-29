import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieComponent } from './movie.component';
import { CardComponent } from './card/card.component';


const routes: Routes = [{
  path: '',
   component: MovieComponent ,
   children: [
    
    { path: 'card/:id', component: CardComponent }

  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MovieRoutingModule { }
