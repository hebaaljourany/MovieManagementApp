import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieService } from '@proxy/movies';
import { MovieDto } from '@proxy/movies'; // Importing the Movie DTO

@Component({
  selector: 'app-details-admin',
  templateUrl: './details-admin.component.html',
  styleUrls: ['./details-admin.component.scss']
})
export class DetailsAdminComponent implements OnInit {
  movieId: string | null = null;
  movieDetails: MovieDto; 

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.movieId = this.route.snapshot.paramMap.get('id');
    if (this.movieId) {
      this.loadMovieDetails(this.movieId);
    }
  }

  loadMovieDetails(movieId: string) {
    this.movieService.get(movieId).subscribe(
      (movie) => {
        this.movieDetails = movie;
        this.movieDetails.releaseDate = this.formatDate(movie.releaseDate);
        this.movieDetails.actors.forEach(actor => {
          actor.thumbnail = "data:image/png;base64," + actor.thumbnail;
        this.movieDetails.actors = movie.actors || [];
        this.movieDetails.categories = movie.categories || [];
        console.log("Actors: ", this.movieDetails.actors);  // تحقق من الممثلين
        console.log("Categories: ", this.movieDetails.categories);  // تحقق من التصنيفات
        this.movieDetails.coverBlob = "data:image/png;base64," + movie.coverBlob;
        this.movieDetails.posterBlob = "data:image/png;base64," +movie.posterBlob;
        });
      },
      (error) => {
        console.error('Error loading movie details:', error);
      }
    );
  }

  backToMovies() {
    this.router.navigate(['/movie-admin']);
  }
  formatDate(date: string): string {
    // Format date to a readable format, e.g., 'dd/MM/yyyy'
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return new Date(date).toLocaleDateString('en-GB', options);
  }
}
