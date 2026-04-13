import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AppService } from "../../../shared/services/app.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { finalize, takeWhile } from "rxjs/operators";
import { BaseComponent } from "src/app/modules/shared/components/base/base.component";
import { AuthService } from "src/app/modules/shared/services/auth.service";
import { environment } from "../../../../../environments/environment";

@Component({
  selector: "main-hangfire-login",
  templateUrl: "./hangfire-login.component.html",
  styleUrls: ["./hangfire-login.component.css"],
})
export class HangfireLoginComponent extends BaseComponent {
  submited: boolean;

  constructor(
    private router: Router,
    private appService: AppService,
    private authService: AuthService
  ) {
    super();
  }

  form = new FormGroup({
    username: new FormControl("", Validators.compose([Validators.required])),
    password: new FormControl("", Validators.required),
  });

  // tslint:disable-next-line:use-lifecycle-interface
  ngOnInit() {}

  send() {
    this.authService
      .loginHangfire(this.form.value)
      .pipe(
        takeWhile(() => this.isAlive),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        this.appService.toastr.info("API: Ok.");
        window.location.href = environment.urls.hangfire;
        if (!resp.success) {
          console.error(resp.error);
          return;
        }
      });
  }
}
