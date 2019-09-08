import { Component, OnInit } from '@angular/core';
import { RssFeedsService, Feed } from '../dashboard-api';

@Component({
  selector: 'app-rss-deed',
  templateUrl: './rss-feed.component.html'
})
export class RssFeedComponent implements OnInit {
  latestPosts: Feed[] = [];

  constructor(private service: RssFeedsService) { }

  ngOnInit() {
    this.service.getAll().subscribe(result => {
      this.latestPosts = result;
    }, error => console.error(error));
  }
}

