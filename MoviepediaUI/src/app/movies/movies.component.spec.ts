import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { Location as TestLocation } from '@angular/common';
import { of } from 'rxjs';
import { Movie } from '../entities/movie';
import { MoviesService } from '../services/movies.service';
import { MovieDetailsComponent } from './movie-details/movie-details.component';

import { MoviesComponent } from './movies.component';

describe('MoviesComponent', () => {
  let component: MoviesComponent;
  let fixture: ComponentFixture<MoviesComponent>;
  let moviesService: jasmine.SpyObj<MoviesService>;
  let movies: Array<Movie> = [
    <Movie>{ title: 'Bend it like Beckham', imdbID: '1', location: 'Kolkata', language: 'English' },
    <Movie>{ title: 'King Leo', imdbID: '2', location: 'Kolkata', language: 'Bengali' }
  ];
  let location: TestLocation;

  beforeEach(() => {
    moviesService = jasmine.createSpyObj<MoviesService>('MoviesService', ['searchMovies', 'filterMovies', 'getMovie']);
    moviesService.searchMovies.and.returnValue(of(movies));
    TestBed.configureTestingModule({
      declarations: [MoviesComponent],
      imports: [
        FormsModule,
        MatInputModule,
        MatFormFieldModule,
        MatButtonModule,
        MatTableModule,
        MatSortModule,
        NoopAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([
          { path: 'movies', component: MoviesComponent },
          { path: 'movies/:id', component: MovieDetailsComponent }])
      ],
      providers: [{ provide: MoviesService, useValue: moviesService }]
    }).compileComponents();

    fixture = TestBed.createComponent(MoviesComponent);
    location = TestBed.get(TestLocation);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initiate data source for table', () => {
    expect(component.movies.data.length).toEqual(movies.length);
    expect(component.movies.data[0].title).toEqual(movies[0].title);
    expect(component.movies.data[1].title).toEqual(movies[1].title);
  });

  it('should initiate distinctLocations', () => {
    expect(component.distinctLocations.length).toEqual(2);
    expect(component.distinctLocations[0]).toEqual('All');
    expect(component.distinctLocations[1]).toEqual('Kolkata');
  });

  it('should initiate distinctLanguages', () => {
    expect(component.distinctLanguages.length).toEqual(3);
    expect(component.distinctLanguages[0]).toEqual('All');
    expect(component.distinctLanguages[1]).toEqual('English');
    expect(component.distinctLanguages[2]).toEqual('Bengali');
  });

  it('should let user search with a title', () => {
    moviesService.searchMovies.calls.reset();
    component.title = 'Tenet';
    const btn = fixture.debugElement.nativeElement.querySelector('.btnSearch');
    fixture.detectChanges();
    btn.click();

    expect(moviesService.searchMovies).toHaveBeenCalledOnceWith('Tenet');
  });

  it('should filter based on the language filter selection', async () => {
    moviesService.filterMovies.and.returnValue(of(movies));
    moviesService.filterMovies.calls.reset();
    const languageSelector = fixture.debugElement.nativeElement.querySelector('[name="language"]');
    languageSelector.value = languageSelector.options[1].value;
    languageSelector.dispatchEvent(new Event('change'));
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(moviesService.filterMovies).toHaveBeenCalledTimes(1);
    });
  });

  it('should filter based on the location filter selection', async () => {
    moviesService.filterMovies.and.returnValue(of(movies));
    moviesService.filterMovies.calls.reset();
    const locationSelector = fixture.debugElement.nativeElement.querySelector('[name="location"]');
    locationSelector.value = locationSelector.options[1].value;
    locationSelector.dispatchEvent(new Event('change'));
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(moviesService.filterMovies).toHaveBeenCalledTimes(1);
    });
  });

  it('should redirect to detail page', fakeAsync(() => {
    component.showDetail(movies[0]);
    tick();

    expect(location.path()).toEqual(`/movies/${movies[0].imdbID}`);
  }));
});
