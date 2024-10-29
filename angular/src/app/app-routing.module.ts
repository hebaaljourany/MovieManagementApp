import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyListComponent } from './movie/my-list/my-list.component';
import { MovieFormComponent } from './movie-admin/movie-form/movie-form.component';
import { DetailsComponent } from './movie/details/details.component';
import { CardComponent } from './movie/card/card.component';
import { ActorFormComponent } from './movie-admin/actor-form/actor-form.component';
import { CategoryFormComponent } from './movie-admin/category-form/category-form.component';
import{DetailsAdminComponent}from'./movie-admin/details-admin/details-admin.component';

const routes: Routes = [
  { path: 'movies', loadChildren: () => import('./movie/movie.module').then(m => m.MovieModule) },
  { path: 'movies/my-list', component: MyListComponent },
  { path: 'movies/details/:id', component: DetailsComponent },
  { path: 'movies/card', component: CardComponent },
  { path: 'movie-admin', loadChildren: () => import('./movie-admin/movie-admin.module').then(m => m.MovieAdminModule) },
  { path: 'movie-admin/movie-form', component: MovieFormComponent },
  { path: 'movie-admin/movie-form/:id', component: MovieFormComponent },
  { path: 'movie-admin/actor-form', component: ActorFormComponent },
  { path: 'movie-admin/category-form', component: CategoryFormComponent },
  { path: 'movie-admin/details-admin', component: DetailsAdminComponent },
  { path: 'movie-admin/details-admin/:id', component: DetailsAdminComponent },





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
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
