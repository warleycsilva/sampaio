import { Component, OnInit } from '@angular/core';
import {BaseComponent} from '../../../shared/components/base/base.component';
import {Router} from '@angular/router';
import {AppService} from '../../../shared/services/app.service';
import {TopBarBreadcrumbService} from 'ngx-chameleon';
import {ProfileService} from '../../../shared/services/profile.service';
import {finalize, takeWhile} from 'rxjs/operators';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CustomValidators} from '../../../shared/validators/custom.validators';
import {Location} from '@angular/common';
@Component({
  // tslint:disable-next-line:component-selector
  selector: 'main-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent extends BaseComponent {
  submited = false;

  show = {
    currentPassword: false,
    newPassword: false,
    newPasswordConfirmation: false
  }

  form = new FormGroup({
    currentPassword: new FormControl('', Validators.required),
    newPassword: new FormControl('', Validators.required),
    newPasswordConfirmation: new FormControl('', Validators.compose([
      Validators.required,
      CustomValidators.match('newPassword')
    ]))
  });

  constructor(
    private router: Router,
    private topBarBreadcrumbService: TopBarBreadcrumbService,
    private appService: AppService,
    private profileService: ProfileService,
    private location: Location
  ) {

    super();
  }

  // tslint:disable-next-line:use-lifecycle-interface
  ngOnInit(): void {
  }
  goBack(){
    this.location.back();
  }

  save() {
    this.submited = true;
    if (this.form.invalid) {
      return;
    }

    this.appService.spinner.show();
    this.profileService.changePassword(this.form.value).pipe(
      takeWhile(() => this.isAlive),
      finalize(() => this.appService.spinner.hide())
    ).subscribe(resp => {
      this.appService.toastr.success(resp.data.message, 'Atenção');
      this.location.back();
    });
  }

}
