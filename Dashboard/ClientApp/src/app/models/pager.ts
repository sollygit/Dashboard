import { ClockService } from "../services/clock.service";
import { Subscription, Subject } from "rxjs";

export class Pager<T extends PagerRow> {
  get page(): Array<T> {
    return this.pages[this.currentPage];
  }

  private allItems: Array<T>;
  private currentPage: number = 0;

  private currentTick: number = 0;
  private subscription: Subscription;
  private rotateSubject = new Subject();

  get items(): Array<T> {
    return this.allItems;
  }

  set items(items: Array<T>) {
    this.allItems = items;
    this.currentPage = -1;
    this.setUpPages();
    this.rotateSubject.next();
  }

  constructor(
    private clockService: ClockService,
    private lifeSpan: number,
    private lineHeight: number,
    private maxHeight: number
  ) {
    this.subscription = this.clockService.updateStream$.subscribe((_update: { previous: Date, current: Date }) => {
      if (this.pages.length <= 1)
        return;
      this.currentTick++;
      if (this.currentTick < this.lifeSpan)
        return;
      this.rotateSubject.next();
      this.currentTick = 0;
    });
    this.subscription.add(this.rotateSubject.subscribe(() => {
      this.currentPage = this.currentPage + 1 < this.pages.length ? this.currentPage + 1 : 0;
    }));
  }

  private pages: Array<Array<T>> = [];

  private setUpPages() {
    this.pages = [];
    let newPage: Array<T> = [];
    let currentHeight = 0;
    for (let i = 0; i < this.items.length; i++) {
      let rowHeight = this.items[i].getHeight(this.lineHeight);
      if (currentHeight + rowHeight > this.maxHeight) {
        this.pages.push(newPage);
        newPage = [];
        currentHeight = 0;
      }
      currentHeight += rowHeight;
      newPage.push(this.items[i]);
    }
    this.pages.push(newPage);
  }

  dispose() {
    this.subscription.unsubscribe();
  }
}

export interface PagerRow {
  getHeight(lineHeight: number): number;
}
