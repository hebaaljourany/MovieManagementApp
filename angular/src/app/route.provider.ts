import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/movies',
        name: '::Menu:Movies',
        iconClass: 'fas fa-watch',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.MyLists"

      },
      {
        path: '/movies/my-list',
        name: '::Menu:MyList',
        iconClass: 'fas fa-list',
        order: 3,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.MyLists"

      },
      {
        path: '/movie-admin',
        name: '::Menu:MoviesList',
        iconClass: 'fas fa-category',
        order: 4,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.Movies.Create"

      },
      {
        path: '/movie-admin/actor-form',
        name: '::Menu:Actors',
        iconClass: 'fas fa-actor',
        order: 6,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.Actors"

      },
      {
        path: '/movie-admin/category-form',
        name: '::Menu:Categories',
        iconClass: 'fas fa-category',
        order: 7,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.Categories"

      },


    ]);
  };
}
