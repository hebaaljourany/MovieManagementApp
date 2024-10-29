import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieAdminComponent } from './movie-admin.component';

describe('MovieAdminComponent', () => {
  let component: MovieAdminComponent;
  let fixture: ComponentFixture<MovieAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovieAdminComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MovieAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
