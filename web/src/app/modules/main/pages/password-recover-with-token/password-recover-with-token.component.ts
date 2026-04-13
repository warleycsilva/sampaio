import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/components/base/base.component';
import {Router, ActivatedRoute} from '@angular/router';
import {AppService} from 'src/app/modules/shared/services/app.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CustomValidators} from 'src/app/modules/shared/validators/custom.validators';
import {finalize, map, takeWhile} from 'rxjs/operators';
import {ProfileService} from '../../../shared/services/profile.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'main-password-recover-with-token',
  templateUrl: './password-recover-with-token.component.html',
  styleUrls: ['./password-recover-with-token.component.css']
})
export class PasswordRecoverWithTokenComponent extends BaseComponent {
  result = {
    error: null,
    success: false
  };

  show = {
    newPassword: false,
    newPasswordConfirmation: false
  };

  submited = false;

  form = new FormGroup({
    newPassword: new FormControl('', Validators.required),
    newPasswordConfirmation: new FormControl('', Validators.compose([
      Validators.required,
      CustomValidators.match('newPassword')
    ])),
    token: new FormControl('', Validators.required)
  });

  constructor(
    private router: Router,
    private actRoute: ActivatedRoute,
    private appService: AppService,
    private profileService: ProfileService
  ) {
    super();
  }

  // tslint:disable-next-line:use-lifecycle-interface
  ngOnInit() {
    const token = this.actRoute.snapshot.params.token;
    if (!token) {
      this.router.navigate(['/']);
      return;
    }
    this.form.get('token').setValue(token);
  }

  save() {
    this.submited = true;

    if (this.form.invalid) {
      return;
    }
    this.appService.spinner.show();
    this.profileService.changePasswordWithToken(this.form.value)
      .pipe(
        takeWhile(() => this.isAlive),
        map(resp => resp.data),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe(resp => {
        this.result = resp;
      });
  }

}
