import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MovieDto } from '@proxy/movies';
import { MovieService } from '@proxy/movies';


@Component({
  selector: 'app-movie-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit {
  @Input() movie: MovieDto;
  isHovered: boolean = false;
  isInList: boolean = false; // لمعرفة إذا كان الفيلم في القائمة

  constructor(private router: Router, private movieService: MovieService) {}

  ngOnInit(): void {
    // تحقق مما إذا كان الفيلم في قائمة المستخدم عند تهيئة الكارد
    this.checkIfInList();
  }

  // تحقق من وجود الفيلم في قائمة المستخدم
  checkIfInList(): void {
    this.movieService.isInMyList(this.movie.id).subscribe((inList: boolean) => {
      this.isInList = inList;
    }, error => {
      console.error('Error checking if movie is in list:', error);
    });
  }

  // عند مرور المؤشر على الكارد
  onMouseEnter(): void {
    this.isHovered = true;
  }

  onMouseLeave(): void {
    this.isHovered = false;
  }

  // الانتقال إلى صفحة تفاصيل الفيلم عند الضغط على الكارد
  goToMovieDetails(): void {
    this.router.navigate(['/movies/details', this.movie.id]);
  }

  // زر التشغيل (لا يقوم بأي شيء حاليًا)
  playMovie(event: Event): void {
    event.stopPropagation(); // منع تفاعل الكارد مع الضغط على الزر
    console.log('Play movie:', this.movie.title);
  }

  // زر إضافة/إزالة الفيلم من القائمة
  toggleMovieInList(event: Event): void {
    event.stopPropagation(); // منع تفاعل الكارد مع الضغط على الزر

    if (!this.isInList) {
      // إضافة الفيلم إلى القائمة
      this.movieService.addToMyList(this.movie.id).subscribe(() => {
        this.checkIfInList(); // إعادة التحقق من حالة الفيلم بعد الإضافة
      }, error => {
        console.error('Error adding movie to list:', error);
      });
    }
  }
}
