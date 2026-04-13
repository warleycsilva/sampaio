import { Injectable } from "@angular/core";
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from "@angular/router";
import { Observable } from "rxjs";
import { Router } from "@angular/router";

import { SessionUser } from "../models/session-user";
import { AppService } from "../services/app.service";

@Injectable({
  providedIn: "root",
})
export class LoginBackofficeGuard implements CanActivate {
  session: SessionUser;

  constructor(private appService: AppService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    this.session = this.appService.sessionUser;

    if (this.session?.type === "Backoffice") {
      this.router.navigate(["backoffice/lessees/map"]);
      return true;
    }
    return false;
  }
}
