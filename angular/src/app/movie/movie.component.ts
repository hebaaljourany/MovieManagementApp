import { Component, HostListener, OnInit } from '@angular/core';
import { GetMovieInputDto, MovieService } from '@proxy/movies';
import { MovieDto } from '@proxy/movies';
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
  providers: [ListService],
})
export class MovieComponent implements OnInit {
  movie = { items: [], totalCount: 0 } as PagedResultDto<MovieDto>;
  isLoading = true;
  currentPage = 1; // Current page number
  pageSize = 0; // Number of items per page
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

  public screenWidth: number; // Variable to store screen width
  public screenHeight: number; // Variable to store screen height
  constructor(
    private movieService: MovieService,
    private router: Router,
    public readonly list: ListService
  ) {
    
    this.screenWidth = window.innerWidth; // Initialize with current width
    this.screenHeight = window.innerHeight; // Initialize with current 
  }

  ngOnInit(): void {
    this.getMovies(); // Fetch movies on initialization
  }
  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.screenWidth = window.innerWidth; // Update width on resize
    this.screenHeight = window.innerHeight; // Update height on resize
    console.log("Height: " + this.screenHeight, "Width: " + this.screenWidth);
    
  }
  getMovies(): void {
    if(this.screenWidth > 1400){
      this.getMovieInput.maxResultCount = 18;
      this.pageSize = 18
    } 
    else{      
      this.getMovieInput.maxResultCount = 12;
      this.pageSize = 12;
    }    
    this.getMovieInput.skipCount = (this.currentPage - 1) * this.pageSize; // Calculate skip count
    this.getMovieInput.maxResultCount = this.pageSize; // Set max result count

    const movieStreamCreator = (query) => this.movieService.getList(this.getMovieInput);

    this.list.hookToQuery(movieStreamCreator).subscribe((response) => {
      console.log(response);
      this.movie = response; //Updating the movie list
      this.isLoading = false; //Stopping the loading state
      this.movie = response;
      this.isLoading = false;
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
  // Method to handle page change
  onPageChange(page: number): void {
    if (page >= 1 && page <= this.totalPages()) { // Ensure valid page number
      this.currentPage = page; // Update current page
      this.getMovies(); // Fetch movies for the new page
    }
  }


  // Calculate total pages based on total count and page size
  totalPages(): number {
    return Math.ceil(this.movie.totalCount / this.pageSize);
  }
}