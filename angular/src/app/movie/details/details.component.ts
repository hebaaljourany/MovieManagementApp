import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MovieService } from '@proxy/movies'; // Importing necessary services
import { MovieDto } from '@proxy/movies'; // Importing the Movie DTO
import { ActorDto } from '@proxy/actors'; // Importing the Actor DTO
import { CategoryDto } from '@proxy/categories'; // Importing the Category DTO

@Component({
  selector: 'app-movie-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  movie: MovieDto; // Holds the movie details
  totalViews: number; // Total views of the movie
  totalDownloads: number; // Total downloads of the movie
  averageRating: number; // Average rating of the movie
  userRating: number; // User's rating for the movie
  actors: ActorDto[]; // List of actors for the movie
  categories: CategoryDto[]; // List of categories for the movie
  selectedMovie = {} as MovieDto;
  movieId: string; // Variable to store the movieId

  constructor(
    private route: ActivatedRoute, // Activated route to get route parameters
    private movieService: MovieService, // Movie service to fetch movie data
    private router: Router // Router for navigation
  ) {}

  ngOnInit(): void {
    // Fetching movie ID from the route parameters
    this.setMovieId();

    // Fetching movie details using the movie service
    this.movieService.get(this.movieId).subscribe({
      next: (movie) => {
        this.movie = movie;
        console.log("Movie ", movie.title); // سيتم طباعة اسم الفيلم بعد تحميل البيانات

        // تعيين الممثلين والتصنيفات
        this.actors = movie.actors || [];
        this.categories = movie.categories || [];
        console.log("Actors: ", movie.actors);  // تحقق من الممثلين
        console.log("Categories: ", movie.categories);  // تحقق من التصنيفات

        // Fetching stats for the movie
        this.getMovieStats();

        // جلب التقييم الخاص بالمستخدم الحالي
        this.getUserRating();
      },
      error: (err) => {
        console.error("Error fetching movie data", err);
      }
    });
  }

  // Method to fetch the movieId from route and store it
  setMovieId(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.movieId = id;
    } else {
      console.error("Movie ID is missing");
    }
  }

  // Method to fetch total views, downloads, and average rating
  getMovieStats(): void {
    this.movieService.getTotalViews(this.movieId).subscribe(totalViews => {
      this.totalViews = totalViews; // Setting total views
      console.log ('test',totalViews)

    });

    this.movieService.getTotalDownloads(this.movieId).subscribe(totalDownloads => {
      this.totalDownloads = totalDownloads; // Setting total downloads
    });

    this.movieService.calculateAverageRating(this.movieId).subscribe(averageRating => {
      this.averageRating = averageRating; // Setting average rating
    });
  }

  getUserRating(): void {
    this.movieService.getUserRating(this.movieId).subscribe({
      next: (rating: number) => {
        this.userRating = rating; // If there's no previous rating, value 0 means no rating
        
      },
      error: (err) => {
        console.error('Error fetching user rating', err);
      }
    });
  }

  // دالة لتقييم الفيلم عند اختيار النجوم
  rateMovie(ratingValue: number): void {
    if (this.movieId) {
      this.movieService.rateMovie(this.movieId, ratingValue).subscribe({
        next: () => {
          console.log(`Movie rated with ${ratingValue} stars`);

          
        },
        error: (err) => {
          console.error('Error rating the movie', err);
        }
      });
      this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([`/movies/details/${this.movieId}`]); // إعادة التوجيه إلى نفس الصفحة
      });
    }
  }
  // Method to navigate to the update movie component
  navigateToUpdate(): void {
    //this.router.navigate(['/movies/update', this.movie.id]); // Navigating to update movie page
    this.router.navigate(['/movies/add-movie', this.movie.id]);

  }
  deleteMovie() {
    if (confirm('Are you sure you want to delete this movie?')) {
      this.movieService.delete(this.movie.id).subscribe(() => {
        this.router.navigate(['/movies']);
      }, error => {
        console.error('Error deleting movie', error);
      });
    }
  }
  
}
