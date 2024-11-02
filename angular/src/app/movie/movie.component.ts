import { Component, OnInit } from '@angular/core';
import { GetMovieInputDto, MovieService } from '@proxy/movies'; // Movie Service to fetch movies
import { MovieDto } from '@proxy/movies'; // Movie Data Transfer Object (DTO)
import { Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { ListService, PagedResultDto, ConfigStateService  } from '@abp/ng.core'; // ABP services for pagination and list handling
import { query } from '@angular/animations';
import { CategoryLookupDto } from '@proxy/movies';
import { ActorLookupDto } from '@proxy/movies';

import { of } from 'rxjs';
import { debounceTime, map, switchMap, distinctUntilChanged } from 'rxjs/operators';


@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss'],
  providers: [
    ListService,
  ],
})
export class MovieComponent implements OnInit {
  movie = { items: [], totalCount: 0 } as PagedResultDto<MovieDto>; // Holds the list of movies and total count
  isLoading = true; // Loading state
  isAdmin = false; // Flag to check if the current user is admin
  getMovieInput = {} as GetMovieInputDto;
  filteredCategories: CategoryLookupDto[] = [];
  selectedCategory: string = '';
  categorySearchTerm: string = ''; // مصطلح البحث الخاص بالتصنيفات
  isDropdownOpen = false;
  hoveredCategory: CategoryLookupDto | null = null;
  filteredActors: ActorLookupDto[] = [];
  selectedActor: string = '';
  actorSearchTerm: string = ''; // مصطلح البحث الخاص بالتصنيفات
  isDropdownActorsOpen = false;
  hoveredActor: ActorLookupDto | null = null;
  movieSearchTerm: string = '';

  constructor(
    private movieService: MovieService, // Injecting Movie Service
    private router: Router, // Injecting Router to navigate
    public readonly list: ListService, // ListService for managing queries
    private domSanitizer: DomSanitizer
  ) {
     //this.videoUrl = 'https://localhost:44350/stream-video';
  }

  ngOnInit(): void {
    const movieStreamCreator = (query) => this.movieService.getList(this.getMovieInput);

    // Hooking the stream to ListService for fetching and updating movie data
    this.list.hookToQuery(movieStreamCreator).subscribe((response) => {
      console.log(response);
      this.movie = response; //Updating the movie list
      this.isLoading = false; //Stopping the loading state
    });
  }

  // Method to navigate to the 'Add Movie' component
  navigateToAddMovie(): void {
    this.router.navigate(['/movies/add-movie']);
  }
  searchCategories(): void {
    if (this.categorySearchTerm.length > 2) {
      this.movieService
        .getCategoryLookup(this.categorySearchTerm)
        .pipe(
          debounceTime(300),
          distinctUntilChanged(),
          map((result) => result.items.slice(0, 10))
        )
        .subscribe((categories) => {
          this.filteredCategories = categories;
        });
    } else {
      this.filteredCategories = [];
    }
  }

  searchActors(): void {
    if (this.actorSearchTerm.length > 2) {
      this.movieService
        .getActorLookup(this.actorSearchTerm)
        .pipe(
          debounceTime(300),
          distinctUntilChanged(),
          map((result) => result.items.slice(0, 10))
        )
        .subscribe((actors) => {
          this.filteredActors = actors;
        });
    } else {
      this.filteredActors = [];
    }
  }


  // تحديد الممثل فقط دون فلترة
selectActor(actor: ActorLookupDto): void {
  this.actorSearchTerm = actor.actorName;
  this.getMovieInput.actorId = actor.id;
  this.filteredActors = [];
  this.isDropdownActorsOpen = false; // إغلاق القائمة بعد الاختيار
}

// تحديد التصنيف فقط دون فلترة
selectCategory(category: CategoryLookupDto): void {
  this.categorySearchTerm = category.categoryName;
  this.getMovieInput.categoryId = category.id;
  this.filteredCategories = [];
  this.isDropdownOpen = false; // إغلاق القائمة بعد الاختيار
}

// دالة لتطبيق الفلترة بناءً على الاختيارات
applyFilters(): void {
  this.isLoading = true;
  this.movieSearchTerm = '';

  this.list.get(); // استدعاء القائمة لتنفيذ الفلترة
}

clearFilters(): void {
  this.actorSearchTerm = '';
  this.categorySearchTerm = '';
  this.getMovieInput.actorId = undefined;
  this.getMovieInput.categoryId = undefined;
  this.filteredActors = [];
  this.filteredCategories = [];
  this.isDropdownActorsOpen = false;
  this.isDropdownOpen = false;
  this.isLoading = true;
  this.list.get();
}

searchMoviesByName(actorName: string):void{
  this.getMovieInput.filter = actorName;
  this.clearFilters();
}

}