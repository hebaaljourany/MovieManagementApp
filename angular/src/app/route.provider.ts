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
        name: 'Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/movies',
        name: 'Movies',
        iconClass: 'fas fa-film',  // Movie icon (for films)
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.MyLists"

      },
      {
        path: '/movies/my-list',
        name: 'MyList',
        iconClass: 'fas fa-list',  // List icon
        order: 3,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.MyLists"

      },
      {
        path: '/movie-admin',
        name: 'Movie Management',
        iconClass: 'fas fa-film',  // Clapperboard icon for movie management
        order: 4,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.Movies.Create"

      },
      {
        path: '/movie-admin/actor-form',
        name: ' Actor Management',
        iconClass: 'fas fa-user',  // Actor icon
        order: 6,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.Actors.Create"

      },
      {
        path: '/movie-admin/category-form',
        name: ' Category Management',
        iconClass: 'fas fa-tags',  // Tag icon for categories
        order: 7,
        layout: eLayoutType.application,
        requiredPolicy : "MovieManagementApp.Categories"

      },


    ]);
  };
}
