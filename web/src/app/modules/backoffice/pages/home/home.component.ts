import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AppService } from "../../../shared/services/app.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { finalize, takeWhile } from "rxjs/operators";
import { BaseComponent } from "src/app/modules/shared/components/base/base.component";
import { AuthService } from "src/app/modules/shared/services/auth.service";
import { environment } from "../../../../../environments/environment";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "main-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent extends BaseComponent {
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
  ngOnInit() {
    const user = this.appService.sessionUser;
  }

  send() {
    this.appService.spinner.show();
    this.authService
      .loginBackoffice(this.form.value)
      .pipe(
        takeWhile(() => this.isAlive),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        if (!resp.success) {
          this.appService.toastr.error(resp.error);
          return;
        }
        if (resp.user.type != "Backoffice") {
          this.appService.toastr.error(
            "Este módulo não está disponível."
          );
        } else {
          this.appService.storeAcsessToken(resp.accessToken);
          this.appService.storeUser(resp.user);
          this.router.navigate(["/backoffice/viagens"]);
        }
      });
  }
}
