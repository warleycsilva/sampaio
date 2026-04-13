import { Component, OnInit } from "@angular/core";
import { SessionUser } from "../../../shared/models/session-user";
import { Router } from "@angular/router";
import { AppService } from "../../../shared/services/app.service";
import { takeWhile } from "rxjs/operators";
import { BaseComponent } from "../../../shared/components/base/base.component";

@Component({
  selector: "main-backoffice-side-bar",
  templateUrl: "./backoffice-side-bar.component.html",
  styleUrls: ["./backoffice-side-bar.component.css"],
})
export class BackofficeSideBarComponent extends BaseComponent {
  user: SessionUser;

  constructor(private router: Router, private appService: AppService) {
    super();
  }

  // tslint:disable-next-line:use-lifecycle-interface
  ngOnInit() {
    this.user = this.appService.sessionUser;
    this.appService.sessionUserEvent
      .pipe(takeWhile(() => this.isAlive))
      .subscribe((user) => (this.user = user));
    if (this.user.type !== "Backoffice") {
      this.signout();
    }
  }

  signout() {
    this.appService.removeAccessToken();
    this.appService.removeUser();
    this.router.navigate(["/backoffice/home"]);
  }
}
