import { Injectable } from "@angular/core";
import {
  CanActivate,
  CanLoad,
  Route,
  UrlSegment,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from "@angular/router";
import { Observable, of } from "rxjs";
import { AppService } from "../services/app.service";

@Injectable({
  providedIn: "root",
})
export class LesseeGuard implements CanActivate, CanLoad {
  constructor(private router: Router, private appService: AppService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const user = this.appService.sessionUser;

    if (
      (user.type === "Lessee" && user?.identification && user.acceptedTerms) ||
      user.isSysAdmin
    ) {
      return of(true);
    }
    return of(false);
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    const user = this.appService.sessionUser;

    if (user.type !== "Lessee") {
      this.appService.removeUser();
      this.appService.removeAccessToken();
      this.router.navigate(["/forbidden"]);
      return of(false);
    }
    return of(true);
  }
}
