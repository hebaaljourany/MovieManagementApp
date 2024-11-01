import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from '@proxy/movies';
import { MovieDto } from '@proxy/movies';

@Component({
  selector: 'app-details-admin',
  templateUrl: './details-admin.component.html',
  styleUrls: ['./details-admin.component.scss']
})
export class DetailsAdminComponent implements OnInit {
  movie: MovieDto | null = null;

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService
  ) {}

  ngOnInit(): void {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.movieService.get(movieId).subscribe((movie) => {
        this.movie = movie;
        this.movie.coverBlob = "data:image/png;base64," + this.movie.coverBlob;
        this.movie.posterBlob = "data:image/png;base64," + this.movie.posterBlob;
      });
    }
  }

  formatDate(date: string): string {
    // Format date to a readable format, e.g., 'dd/MM/yyyy'
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return new Date(date).toLocaleDateString('en-GB', options);
  }
}
