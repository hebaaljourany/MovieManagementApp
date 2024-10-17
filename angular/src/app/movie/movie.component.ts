import { Component, OnInit } from '@angular/core';
import { MovieService } from '@proxy/movies'; // Movie Service to fetch movies
import { MovieDto } from '@proxy/movies'; // Movie Data Transfer Object (DTO)
import { Router } from '@angular/router';
import { ListService, PagedResultDto } from '@abp/ng.core'; // ABP services for pagination and list handling

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

  constructor(
    private movieService: MovieService, // Injecting Movie Service
    private router: Router, // Injecting Router to navigate
    public readonly list: ListService // ListService for managing queries
  ) {}

  ngOnInit(): void {
    // Stream to fetch the list of movies using a query
    const movieStreamCreator = (query) => this.movieService.getList(query);

    // Hooking the stream to ListService for fetching and updating movie data
    this.list.hookToQuery(movieStreamCreator).subscribe((response) => {
      console.log(response);
      
      this.movie = response; // Updating the movie list
      this.isLoading = false; // Stopping the loading state
    });
  }

  // Method to navigate to the 'Add Movie' component
  navigateToAddMovie(): void {
    this.router.navigate(['/movies/add-movie']);
  }
}
