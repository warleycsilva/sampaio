import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  CanActivate,
  CanLoad,
  Route,
  UrlSegment,
} from "@angular/router";
import { Observable, of } from "rxjs";
import { AppService } from "../services/app.service";

@Injectable({
  providedIn: "root",
})
export class RenterGuard implements CanActivate, CanLoad {
  constructor(private router: Router, private appService: AppService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    const user = this.appService.sessionUser;

    if ((user.type === "Renter" && user.acceptedTerms) || user.isSysAdmin) {
      return of(true);
    }
    return of(false);
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    const user = this.appService.sessionUser;
    if (!user) {
      this.router.navigate(["/login"]);
      return false;
    }

    if (user.type !== "Renter") {
      this.appService.removeUser();
      this.appService.removeAccessToken();
      this.router.navigate(["/forbidden"]);
      return false;
    }
    return true;
  }
}
