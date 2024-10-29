import { Component, OnInit } from '@angular/core';
import { MovieService } from '@proxy/movies'; // Movie Service to fetch movies
import { MovieDto } from '@proxy/movies'; // Movie Data Transfer Object (DTO)
import { Router } from '@angular/router';
import { ListService, PagedResultDto,  } from '@abp/ng.core'; // ABP services for pagination and list handling


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

  constructor(
    private movieService: MovieService, // Injecting Movie Service
    private router: Router, // Injecting Router to navigate
    public readonly list: ListService, // ListService for managing queries

  ) {}

  ngOnInit(): void {

    const movieStreamCreator = (query) => this.movieService.getList(query);

    // Hooking the stream to ListService for fetching and updating movie data
    this.list.hookToQuery(movieStreamCreator).subscribe((response) => {
      console.log(response);
      this.movie = response; //Updating the movie list
      this.isLoading = false; //Stopping the loading state
    });

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
