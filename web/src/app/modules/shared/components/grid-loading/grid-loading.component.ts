import { Component, OnInit, Input } from '@angular/core';
import { PagedListService } from 'ngx-paged-list';

@Component({
  selector: 'grid-loading',
  templateUrl: './grid-loading.component.html',
  styleUrls: ['./grid-loading.component.css']
})
export class GridLoadingComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  @Input('grid') grid: PagedListService;
}
