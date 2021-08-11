import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { Location as TestLocation } from '@angular/common';
import { of } from 'rxjs';
import { Movie } from 'src/app/entities/movie';
import { MoviesService } from 'src/app/services/movies.service';
import { MoviesComponent } from '../movies.component';

import { MovieDetailsComponent } from './movie-details.component';

describe('MovieDetailsComponent', () => {
  let component: MovieDetailsComponent;
  let fixture: ComponentFixture<MovieDetailsComponent>;
  let moviesService: jasmine.SpyObj<MoviesService>;
  let movie: Movie = <Movie>{ title: 'Bend it like Beckham', imdbID: '1', location: 'Kolkata', language: 'English' };
  let location: TestLocation;

  beforeEach(() => {
    moviesService = jasmine.createSpyObj<MoviesService>('MoviesService', ['searchMovies', 'filterMovies', 'getMovie']);
    moviesService.getMovie.and.returnValue(of(movie));

    TestBed.configureTestingModule({
      declarations: [MovieDetailsComponent],
      imports: [
        FormsModule,
        MatFormFieldModule,
        MatButtonModule,
        NoopAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([
          { path: 'movies', component: MoviesComponent },
          { path: 'movies/:id', component: MovieDetailsComponent }])
      ],
      providers: [{ provide: MoviesService, useValue: moviesService }]
    })
      .compileComponents();

    fixture = TestBed.createComponent(MovieDetailsComponent);
    location = TestBed.get(TestLocation);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getMovie to load data', () => {
    expect(moviesService.getMovie).toHaveBeenCalledTimes(1);
  });

  it('should redirect to movies page when back button is clicked', fakeAsync(() => {
    const btn = fixture.debugElement.nativeElement.querySelector('button');
    btn.click();
    tick();

    expect(location.path()).toEqual('/movies');
  }));
});
