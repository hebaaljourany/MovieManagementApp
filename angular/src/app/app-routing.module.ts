import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyListComponent } from './movie/my-list/my-list.component';
import { AddMovieComponent } from './movie/add-movie/add-movie.component';
import { DetailsComponent } from './movie/details/details.component';
import { UpdateMovieComponent } from './movie/update-movie/update-movie.component';
import { CardComponent } from './movie/card/card.component';

const routes: Routes = [
  { path: 'movies', loadChildren: () => import('./movie/movie.module').then(m => m.MovieModule) },
  { path: 'movies/my-list', component: MyListComponent },
  { path: 'movies/add-movie', component: AddMovieComponent },
  { path: 'movies/details/:id', component: DetailsComponent },
  { path: 'movies/update-movie', component: UpdateMovieComponent },
  { path: 'movies/card', component: CardComponent },
  { path: 'movies/add-movie/:id', component: AddMovieComponent },

  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  { path: 'movies', loadChildren: () => import('./movie/movie.module').then(m => m.MovieModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
