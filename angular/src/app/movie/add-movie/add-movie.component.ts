import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovieService } from '@proxy/movies';
import { ActivatedRoute, Router } from '@angular/router'; // ملاحظة: استيراد ActivatedRoute للوصول إلى معرف الفيلم
import { ActorLookupDto, CategoryLookupDto } from '@proxy/movies';
import { Observable, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.scss']
})
export class AddMovieComponent implements OnInit {
  movieForm: FormGroup;
  actorSearchTerm: string = '';
  categorySearchTerm: string = '';
  filteredActors$: Observable<ActorLookupDto[]>;
  filteredCategories$: Observable<CategoryLookupDto[]>;
  selectedActors: ActorLookupDto[] = [];
  selectedCategories: CategoryLookupDto[] = [];
  movieId: string | null = null;  // لمعرفة إذا كان الفيلم للتعديل أو الإضافة

  constructor(
    private fb: FormBuilder,
    private movieService: MovieService,
    private router: Router,
    private route: ActivatedRoute // للوصول إلى معرف الفيلم من المسار
  ) {}

  ngOnInit(): void {
    this.buildCreateForm();
    
    // التحقق إذا كان المعرف موجود في المسار (حالة تعديل)
    this.movieId = this.route.snapshot.paramMap.get('id');
    if (this.movieId) {
      this.loadMovieData(this.movieId);
    }
  }

  buildCreateForm() {
    this.movieForm = this.fb.group({
      title: ['', Validators.required],
      duration: [null, [Validators.required, Validators.min(1), Validators.max(300)]],
      description: [''],
      ageRating: ['', Validators.required],
      releaseDate: ['', Validators.required],
      posterUrl: ['', Validators.required],
      videoUrl: ['', Validators.required],
      actorSearchTerm: [''],  
      categorySearchTerm: ['']
    });
  }
  loadMovieData(movieId: string) {
    this.movieService.get(movieId).subscribe(movie => {
      console.log('Loaded movie:', movie);
      console.log('Actors:', movie.actors);
      console.log('Categories:', movie.categories);
      
      this.movieForm.patchValue(movie);
      this.selectedActors = movie.actors; // تعيين الممثلين الحاليين
      this.selectedCategories = movie.categories; // تعيين التصنيفات الحالية
    });
  }

  searchActors() {
    const searchTerm = this.movieForm.get('actorSearchTerm')?.value;
    if (searchTerm && searchTerm.length > 2) {
      this.filteredActors$ = this.movieService.getActorLookup(searchTerm).pipe(
        debounceTime(300),
        distinctUntilChanged(),
        map(result => result.items.slice(0, 10))
      );
    } else {
      this.filteredActors$ = of([]);
    }
  }

  addActor(actor: ActorLookupDto) {
    if (!this.selectedActors.some(a => a.id === actor.id)) {
      this.selectedActors.push(actor);
    }
    this.movieForm.get('actorSearchTerm')?.setValue('');
  }

  removeActor(actor: ActorLookupDto) {
    const index = this.selectedActors.findIndex(a => a.id === actor.id);
    if (index >= 0) {
      this.selectedActors.splice(index, 1);
    }
  }

  searchCategories() {
    const searchTerm = this.movieForm.get('categorySearchTerm')?.value;
    if (searchTerm && searchTerm.length > 2) {
      this.filteredCategories$ = this.movieService.getCategoryLookup(searchTerm).pipe(
        debounceTime(300),
        distinctUntilChanged(),
        map(result => result.items.slice(0, 10))
      );
    } else {
      this.filteredCategories$ = of([]);
    }
  }

  addCategory(category: CategoryLookupDto) {
    if (!this.selectedCategories.some(c => c.id === category.id)) {
      this.selectedCategories.push(category);
    }
    this.movieForm.get('categorySearchTerm')?.setValue('');
  }

  removeCategory(category: CategoryLookupDto) {
    const index = this.selectedCategories.findIndex(c => c.id === category.id);
    if (index >= 0) {
      this.selectedCategories.splice(index, 1);
    }
  }

  onSubmit() {
    if (this.movieForm.invalid) {
      return;
    }

    const movieData = {
      ...this.movieForm.value,
      actorIds: this.selectedActors.map(actor => actor.id),
      categoryIds: this.selectedCategories.map(category => category.id)
    };

    if (this.movieId) {
      // إذا كان تعديل فيلم
      this.movieService.update(this.movieId, movieData).subscribe(
        (response) => {
          this.router.navigate(['/movies/details', response.id]);
        },
        (error) => {
          console.error('An error occurred while updating the movie:', error);
        }
      );
    } else {
      // إذا كان إنشاء فيلم جديد
      this.movieService.create(movieData).subscribe(
        (response) => {
          this.router.navigate(['/movies/details', response.id]);
        },
        (error) => {
          console.error('An error occurred while creating the movie:', error);
        }
      );
    }
  }

  closeForm() {
    this.router.navigate(['/movies']);
  }
}
