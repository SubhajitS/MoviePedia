import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MoviesService } from './movies.service';
import { environment } from 'src/environments/environment';
import { Movie } from '../entities/movie';

describe('MoviesService', () => {
  let service: MoviesService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(MoviesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('searchMovies should call search endpoint with GET request and return the matching movies', () => {
    const title = 'Tenet';
    service.searchMovies(title).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/search?title=${title}`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('searchMovies should call search endpoint with empty querystring param if title is null', () => {
    const title = null;
    service.searchMovies(title).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/search?title=`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('searchMovies should call search endpoint with empty querystring param if title is undefined', () => {
    const title = undefined;
    service.searchMovies(title).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/search?title=`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('filterMovies should call filter endpoint with GET request and Language and Location param', () => {
    const Language = 'Spanish';
    const Location = 'Barcelona';
    service.filterMovies(Language, Location).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/filter?language=${Language}&location=${Location}`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('filterMovies should call filter endpoint with GET request and Language if Location is undefined', () => {
    const Language = 'Spanish';
    const Location = undefined;
    service.filterMovies(Language, Location).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/filter?language=${Language}&location=`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('filterMovies should call filter endpoint with GET request and Language if Location is All', () => {
    const Language = 'Spanish';
    const Location = 'All';
    service.filterMovies(Language, Location).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/filter?language=${Language}&location=`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('filterMovies should call filter endpoint with GET request and Location if Language is undefined', () => {
    const Language = undefined;
    const Location = 'Barcelona';
    service.filterMovies(Language, Location).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/filter?language=&location=${Location}`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('filterMovies should call filter endpoint with GET request and Location if Language is All', () => {
    const Language = 'All';
    const Location = 'Barcelona';
    service.filterMovies(Language, Location).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/filter?language=&location=${Location}`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('filterMovies should call filter endpoint with GET request if Location and Language are All', () => {
    const Language = 'All';
    const Location = 'All';
    service.filterMovies(Language, Location).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/filter?language=&location=`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });

  it('filterMovies should call filter endpoint with GET request if Location and Language are undefined', () => {
    const Language = undefined;
    const Location = undefined;
    service.filterMovies(Language, Location).subscribe(x => {
      expect(x.length).toEqual(1);
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/filter?language=&location=`);
    expect(req.request.method).toEqual('GET');
    req.flush([<Movie>{ title: 'The Hitman', imdbID: 'imdb1'}]);

    httpMock.verify();
  });
  
  it('getMovie should call Movies endpoint with GET request with movie ID', () => {
    const id = 'movieID1';
    service.getMovie(id).subscribe(x => {
      expect(x.title).toEqual('The Hitman');
    });

    const req = httpMock.expectOne(`${environment.endpoint}movies/${id}`);
    expect(req.request.method).toEqual('GET');
    req.flush(<Movie>{ title: 'The Hitman', imdbID: id});

    httpMock.verify();
  });

  it('getMovie should throw error if id is null/undefined', () => {
    const id = undefined;

    expect(() => service.getMovie(id)).toThrowError('id cannot be null or undefined');
    
    httpMock.expectNone(`${environment.endpoint}movies/${id}`);

    httpMock.verify();
  });
});
