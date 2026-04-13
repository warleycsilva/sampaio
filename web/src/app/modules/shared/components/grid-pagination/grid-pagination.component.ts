import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { PagedListService } from 'ngx-paged-list';

@Component({
  selector: '[grid-pagination]',
  templateUrl: './grid-pagination.component.html',
  styleUrls: ['./grid-pagination.component.css']
})
export class GridPaginationComponent implements OnInit, OnChanges {

  constructor() { }

  ngOnInit() {
    if (this.grid) {
      this.currentPage = this.grid.pageIndex;
      this.totalPages = this.grid.totalPages;
      this.handlePages()
    }
  }

  ngOnChanges(simpleChanges: SimpleChanges) {
    if ((simpleChanges.currentPage && simpleChanges.currentPage.currentValue != simpleChanges.currentPage.previousValue) ||
      (simpleChanges.totalPages && simpleChanges.totalPages.currentValue != simpleChanges.totalPages.previousValue)) {
      this.handlePages();
    }
  }

  @Input('grid') grid: PagedListService;

  @Input('current-page') currentPage: number = 0;

  @Input('total-pages') totalPages: number = 0;

  @Input('max-size') maxSize: number = 9;

  private handlePages() {
    let startPage: number, endPage: number;
    if (this.totalPages <= this.maxSize) {
      startPage = 1;
      endPage = this.totalPages;
    } else {
      if (this.currentPage <= 6) {
        startPage = 1;
        endPage = 10;
      } else if (this.currentPage + 4 >= this.totalPages) {
        startPage = this.totalPages - 9;
        endPage = this.totalPages;
      } else {
        startPage = this.currentPage - 5;
        endPage = this.currentPage + 4;
      }
    }
    this.pages = Array.from(Array((endPage + 1) - startPage).keys()).map(i => startPage + i);
  }

  pages: number[] = [];

  goToPage(page: number) {
    if(page == this.grid.pageIndex) return;
    this.grid.pageIndex = page;
    this.grid.load();
  }
}
