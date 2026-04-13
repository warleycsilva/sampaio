import { Component } from '@angular/core';
import { AppService } from '../shared/services/app.service';
import { BaseComponent } from '../shared/components/base/base.component';

@Component({
  selector: 'main-root',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent extends BaseComponent {


  constructor(
    private appService: AppService) {
    super();
  }

}
