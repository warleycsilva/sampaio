import { Component, OnInit } from "@angular/core";
import { BaseComponent } from "../../../shared/components/base/base.component";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { CustomValidators } from "../../../shared/validators/custom.validators";
import { ActivatedRoute, Router } from "@angular/router";
import { AppService } from "../../../shared/services/app.service";
import { ProfileService } from "../../../shared/services/profile.service";
import { finalize, map, takeWhile } from "rxjs/operators";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "main-activate-account",
  templateUrl: "./activate-account.component.html",
  styleUrls: ["./activate-account.component.css"],
})
export class ActivateAccountComponent extends BaseComponent {
  result = {
    message: null,
    error: null,
    success: false,
    userProfile: null,
  };

  submited = false;

  form = new FormGroup({
    token: new FormControl("", Validators.required),
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
  ngOnInit(): void {
    const token = this.actRoute.snapshot.params.token;
    if (!token) {
      this.router.navigate(["/"]);
      return;
    }
    this.form.get("token").setValue(token);
    this.send();
  }

  send() {
    this.submited = true;

    if (this.form.invalid) {
      return;
    }
    this.appService.spinner.show();

    this.profileService
      .activateAccount(this.form.value)
      .pipe(
        takeWhile(() => this.isAlive),
        map((resp) => resp.data),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        this.result = resp;
      });
  }
}
