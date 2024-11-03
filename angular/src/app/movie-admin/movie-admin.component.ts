import { Component, OnInit } from '@angular/core';
import { GetMovieInputDto, MovieService } from '@proxy/movies';
import { MovieDto } from '@proxy/movies'; // Movie Data Transfer Object (DTO)
import { Router } from '@angular/router';
import { ListService, PagedResultDto,  } from '@abp/ng.core'; // ABP services for pagination and list handling
import { CategoryLookupDto } from '@proxy/movies';
import { ActorLookupDto } from '@proxy/movies';
import { debounceTime, map, switchMap, distinctUntilChanged } from 'rxjs/operators';


@Component({
  selector: 'app-movie-admin',
  templateUrl: './movie-admin.component.html',
  styleUrls: ['./movie-admin.component.scss'],
  providers: [
    ListService,
  ],
})
export class MovieAdminComponent implements OnInit {
  movie = { items: [], totalCount: 0 } as PagedResultDto<MovieDto>; // Holds the list of movies and total count
  isLoading = true; // Loading state
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

  ) {}

  ngOnInit(): void {
    const movieStreamCreator = (query) => this.movieService.getList(this.getMovieInput);
    // Hooking the stream to ListService for fetching and updating movie data
    this.list.hookToQuery(movieStreamCreator).subscribe((response) => {
      console.log(response);
      this.movie = response; //Updating the movie list
      this.isLoading = false; //Stopping the loading state
    });

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

  // Method to navigate to the 'Add Movie' component
  navigateToAddMovie(): void {
    this.router.navigate(['/movie-admin/movie-form']);
  }
  // Method to navigate to the edit movie component
  editMovie(movieId: string): void {
    this.router.navigate(['/movie-admin/movie-form', movieId]);
  }

  // Method to delete a movie
  delete(movieId: string): void {
    // Call delete movie service
    this.movieService.delete(movieId).subscribe(() => {
      this.ngOnInit(); // Refresh the movie list after deletion
    });
  }

  // Method to view details of the movie
  viewDetails(movieId: string): void {
    this.router.navigate(['/movie-admin/details-admin', movieId]);
  }
  onRowActivate(event: any) {
    // Assuming `getdetails` function exists in your component
    if (event.type === 'click' ) {
      this.router.navigate(['/movie-admin/details-admin', event.row.id]);

    }

  }
}
