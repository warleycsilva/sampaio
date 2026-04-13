import { Injectable } from "@angular/core";
import { CanLoad, Route, UrlSegment, Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate } from "@angular/router";
import { Observable, of } from "rxjs";
import { SessionUser } from "../models/session-user";
import { AppService } from "../services/app.service";

@Injectable({
  providedIn: "root",
})
export class PartnerGuard implements CanActivate {
  
  constructor(private router: Router, private appService: AppService) {}

  session: SessionUser;
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    this.session = this.appService.sessionUser;

    if (this.session?.type !== "Partner") {
      this.router.navigate(["parcerias/login"]);
    }

    return true;
  }
}
