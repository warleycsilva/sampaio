import { Component, OnInit } from "@angular/core";
import { TopBarBreadcrumbService } from "ngx-chameleon";
import { BaseComponent } from "../../../shared/components/base/base.component";
import { Router } from "@angular/router";
import { AppService } from "../../../shared/services/app.service";
import { ProfileService } from "../../../shared/services/profile.service";
import { finalize, map, takeWhile } from "rxjs/operators";

@Component({
  selector: "main-term-and-conditions",
  templateUrl: "./term-and-conditions.component.html",
  styleUrls: ["./term-and-conditions.component.css"],
})
export class TermAndConditionsComponent
  extends BaseComponent
  implements OnInit
{
  constructor(
    private topBarBreadcrumbService: TopBarBreadcrumbService,
    private router: Router,
    private appService: AppService,
    private profileService: ProfileService
  ) {
    super();
  }

  ngOnInit(): void {
    this.topBarBreadcrumbService.emmiter.next({
      icon: "fas fa-file-signature icon fa-icon",
      title: "Termos e condições",
      path: ["Termos e condições"],
    });
  }

  accept() {
    this.profileService
      .acceptTerms()
      .pipe(
        takeWhile(() => this.isAlive),
        map((resp) => resp.data),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        if (resp.success) {
          this.appService.toastr.success(
            "Termos aceitos com sucesso.\nComplete a documentação para prosseguir"
          );
          this.appService.removeAccessToken();
          this.appService.removeUser();

          this.appService.storeAcsessToken(resp.accessToken);
          this.appService.storeUser(resp.user);

          if (this.appService.sessionUser?.identification) {
            return this.router.navigate(["renter/home"]);
          } else {
            return this.router.navigate(["/complete-signup"]);
          }
        }
      });
  }

  signout() {
    this.appService.removeAccessToken();
    this.appService.removeUser();
    this.router.navigate(["login"]);
  }
}
