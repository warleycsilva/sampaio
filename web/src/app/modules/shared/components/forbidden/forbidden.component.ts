import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/modules/shared/components/base/base.component';
import { AppService } from 'src/app/modules/shared/services/app.service';
import { TopBarBreadcrumbService } from 'ngx-chameleon';
import { takeWhile } from 'rxjs/operators';

@Component({
  selector: 'main-forbidden',
  templateUrl: './forbidden.component.html',
  styleUrls: ['./forbidden.component.css']
})
export class ForbiddenComponent extends BaseComponent {

  constructor(
    private appService: AppService,
    private topBarBreadcrumbService: TopBarBreadcrumbService
  ) {
    super();
  }

  ngOnInit() {
    this.topBarBreadcrumbService.emmiter.next({
      icon: 'fal fa-ban fa-icon',
      title: 'Acesso Probíbido',
      path: ['Acesso Probíbido']
    });
  }

}
