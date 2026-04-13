import { Component, Input } from '@angular/core';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'login-layout',
  templateUrl: './login-layout.component.html',
  styleUrls: ['./login-layout.component.css']
})
export class LoginLayoutComponent extends BaseComponent {

  constructor() {
    super();
  }

  ngOnInit() {
  }

  @Input()title = '';

  @Input()subTitle = '';

  @Input()showTitle = true;

  @Input()showLogo = true;

  @Input()showLangOptions = true;
}
