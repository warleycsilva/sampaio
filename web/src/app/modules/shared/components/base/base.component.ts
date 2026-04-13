import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-base',
  template: '<p>base works!</p>'
})
export class BaseComponent implements OnInit, OnDestroy {

  constructor() { }

  ngOnInit() {
  }

  ngOnDestroy = () => this.isAlive = false;

  protected isAlive: boolean = true;

}
