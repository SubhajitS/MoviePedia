import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Movie } from '../entities/movie';
import { MoviesService } from '../services/movies.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})
export class MoviesComponent implements OnInit {

  constructor(private movieSvc: MoviesService, private router: Router) { }

  title: string;
  movies: MatTableDataSource<Movie>;
  displayedColumns: string[] = ['title', 'listingType', 'language', 'location'];
  distinctLanguages: string[] = ['All'];
  distinctLocations: string[] = ['All'];
  selectedLanguage: string = 'All';
  selectedLocation: string = 'All';

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit(): void {
    this.GetMovies();
  }

  searchByTitle() {
    this.GetMovies();
  }

  showDetail(selectedMovie: Movie) {
    this.router.navigate(['movies', selectedMovie.imdbID]);
  }

  change() {
    this.movieSvc.filterMovies(this.selectedLanguage, this.selectedLocation).subscribe(x => {
      this.movies = new MatTableDataSource<Movie>(x);
      this.movies.sort = this.sort;
    });
  }

  private GetMovies() {
    this.movieSvc.searchMovies(this.title).subscribe(x => {
      this.movies = new MatTableDataSource<Movie>(x);
      this.distinctLocations = ['All', ...x.map(x => x.location).filter((v, i, s) => s.indexOf(v) === i)];
      this.distinctLanguages = ['All', ...x.map(x => x.language).filter((v, i, s) => s.indexOf(v) === i)];
      this.movies.sort = this.sort;
    });
  }

}
