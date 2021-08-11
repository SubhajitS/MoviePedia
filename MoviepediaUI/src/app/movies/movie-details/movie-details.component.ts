import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.scss']
})
export class MovieDetailsComponent implements OnInit, OnDestroy {

  private subscription: Subscription;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe(params => console.log(params));
  }

  ngOnDestroy() {
    if (this.subscription && !this.subscription.closed)
      this.subscription.unsubscribe();
  }
}
