import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Movie } from '../entities/movie';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  constructor(private httpClient: HttpClient) { }

  searchMovies(title: string): Observable<Array<Movie>> {
    const param = new HttpParams().append('title', title ?? '');
    return this.httpClient.get<Array<Movie>>(`${environment.endpoint}movies/search`, { params: param });
  }

  filterMovies(language: string, location: string): Observable<Array<Movie>> {
    const param = new HttpParams().append('language', (!language || language === 'All') ? '' : language)
      .append('location', (!location || location === 'All') ? '' : location);
    return this.httpClient.get<Array<Movie>>(`${environment.endpoint}movies/filter`, { params: param });
  }

  getMovie(id: string): Observable<Movie> {
    if (!id)
      throw new Error('id cannot be null or undefined');

    return this.httpClient.get<Movie>(`${environment.endpoint}movies/${id}`);
  }
}
