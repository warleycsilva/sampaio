import { Injectable } from "@angular/core";
import { CanLoad, Route, UrlSegment, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { AppService } from "../services/app.service";

@Injectable({
  providedIn: "root",
})
export class EstablishmentLoadGuard implements CanLoad {
  constructor(private router: Router, private appService: AppService) {}

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    const user = this.appService.sessionUser;

    if (user && user?.type !== "Establishment") {
      //this.appService.removeUser();
      //this.appService.removeAccessToken();
      this.router.navigate(["/forbidden"]);
      return false;
    }
    return true;
  }
}
