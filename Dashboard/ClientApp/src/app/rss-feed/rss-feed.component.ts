import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-rss-deed',
  templateUrl: './rss-feed.component.html'
})
export class RssFeedComponent implements OnInit {
  latestPosts: Feed[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.http.get<Feed[]>(this.baseUrl + 'api/RssFeeds/GetAll').subscribe(result => {
      this.latestPosts = result;
    }, error => console.error(error));
  }
}

interface Feed {  
  link: string;  
  title: string;  
  feedType: string;  
  author: string;  
  content: string;  
  publishDate: string;  
}

