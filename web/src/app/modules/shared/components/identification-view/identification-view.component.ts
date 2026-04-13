import { Component, OnInit, Input } from '@angular/core';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'identification-view',
  templateUrl: './identification-view.component.html',
  styleUrls: ['./identification-view.component.css']
})
export class IdentificationViewComponent extends BaseComponent {

  constructor() {
    super()
  }

  ngOnInit() {
  }

  @Input('data') identification: any;
}
