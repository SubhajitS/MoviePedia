import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { mergeMap } from 'rxjs/operators'; 
import { Movie } from 'src/app/entities/movie';
import { MoviesService } from 'src/app/services/movies.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.scss']
})
export class MovieDetailsComponent implements OnInit, OnDestroy {

  private subscription: Subscription;
  movie: Movie
  constructor(private movieSvc: MoviesService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.subscription = this.route.params.pipe(mergeMap(params => {
      return this.movieSvc.getMovie(params.id);
    }))
    .subscribe(mov => this.movie = mov);
  }

  ngOnDestroy() {
    if (this.subscription && !this.subscription.closed)
      this.subscription.unsubscribe();
  }

  back() {
    this.router.navigate(['movies']);
  }
}
